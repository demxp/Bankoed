using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using Utilities;

namespace BANCOED
{
    public class Memory
    {
        // Импортируем функцию для чтения памяти чужого процесса 
        // из kernel32
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] buffer,
            UInt32 size,
            out IntPtr lpNumberOfBytesRead
            );

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            UInt32 size,
            IntPtr lpNumberOfBytesWritten
            );

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String ClassName, String WindowName);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        public int CurrProc = 0;
        public String PlayerName = "";
        public String TargetName = "";
        public int CHP = 0;
        public int CMP = 0;
        public int CFLY = 0;
        public bool Status = false;
        public int percHP = 0;
        public int percMP = 0;
        public int percAtkSpeed = 100;
        public int flyVal = 0;
        public int flyCD = 0;
        public bool autoCrb = false;
        public bool autoTarget = false;
        public bool startHPMP = false;
        public bool startFLY = false;
        public bool AtcSpeed = false;
        public int Priority = 0;
        public string Log = "";
        public int PlayerAtkSpeed = 0;
        public bool BindWindow = true;
        public uint hppot = 0;
        public uint mppot = 0;
        public uint flpot = 0;

        public bool pause = false;

        public string TName = "";
        public int THPmax = 0;
        public int THPmin = 0;
        public int TLvl = 0;


        // Определение базового адреса и оффсетов
        // для текущей версии клиента руоффа (130)
        private int baseAddress = 0,
                      offsetHP = 0xEFE178,
                      offsetMP = 0xEFE180,
                      offsetMaxHP = 0xEFE174,
                      offsetMaxMP = 0xEFE17C,
                      offsmaxfly = 0xEFE188,
                      offscurrfly = 0xEFE18C,
                      offstarget1 = 0xAA95A0,
                      offstarget2 = 0x258,
                      offstarget_name = 0x3E,
                      offstarget_id = 0x28,
                      offstarget_type = 0x18,
                      offstarget_gatherable = 0x192,
                      offstargetgathering = 0xEF7068,
                      offstargetcasting = 0xB6C,
                      offstargetstate = 0x2B0,
                      offstargetHP = 0x12CC,
                      offstargetMaxHP = 0x12D0,
                      offstargetLvl = 0x3A,
                      offsplayerid = 0xEFE4E8;
        private int offsplayercoords = 0x134;
        private int offsplayercoordsz = 0x6C;
        private int offsplayercoordsx = 0x64;
        private int offsplayercoordsy = 0x68;
        private int offsplayerrotation = 0xEF3E2C;
        private Int32 PlayerOffset = 0;
        private int offsFly1 = 0xBE8;
        private int offsFly2 = 0xBDC; //??
        private int offsAtcSpeed = 0x4DA;
        private int offsStaticAtcSpeed = 0xEFE288;

        private int GameTimer = 0;
        //private int OffsetPotionHpUsed = 0;
        //private int OffsetPotionMpUsed = 0;
        //private bool InventoryScanned = false;

        // Служебная переменная для хранения PID процесса
        private Int32 PID = 0;
        private IntPtr pHandle = IntPtr.Zero;
        private IntPtr hWnd;
        private System.Windows.Forms.Timer updateTimer;

        private Int32 HP { get; set; }
        private Int32 MP { get; set; }
        private Int32 MaxHP { get; set; }
        private Int32 MaxMP { get; set; }
        private Int32 Fly { get; set; }
        private Int32 MaxFly { get; set; }
        public Int32 PlayerId { get; set; }
        private byte GatherStatus;
        private int GathStart = 0;
        private Int32 Targ1 = 0;
        private Int32 Targ2 = 0;
        private Int32 TargetId;
        private Int32 TargetType;
        private byte TargetGatherable;
        private int PlayerState;
        private Int32 PlayerTargId;
        private int PlayerCasting;
        private int TargetHP;
        private int TargetMaxHP;
        private int TargetLvl;

        public bool NoGrav = false;

        private globalKeyboardHook gkh = new globalKeyboardHook();
        public InventoryScanner inv = new InventoryScanner();

        public delegate void ShowMess(string Mess);
        public event ShowMess onMessage;

        private void ResetData()
        {
            PlayerName = "";
            TargetName = "";
            CHP = 0;
            CMP = 0;
            CFLY = 0;
            TargetDataSet(true);
        }

        public bool Monitor()
        {
            ResetData();
            if (initializeWindow())
            {
                updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Enabled = true;
                updateTimer.Interval = 100;
                updateTimer.Tick += new EventHandler(updateTimer_Tick);
                Status = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Данный метод вызывается каждый тик таймера
        /// Здесь происходит чтение из памяти клиента значений
        /// жизни и маны
        /// </summary>
        /// 

        private bool initializeWindow()
        {
            System.Diagnostics.Process[] HandleP = System.Diagnostics.Process.GetProcessesByName("aion.bin");
            hWnd = FindWindow(null, "AION Client");

            if (HandleP.Length == 0)
            {
                return false;
            }

            PID = HandleP[CurrProc].Id;
            pHandle = HandleP[CurrProc].Handle;
            NoGrav = false;
            startHPMP = false;
            startFLY = false;
            AtcSpeed = false;
            inv.Scanned = false;

            foreach (System.Diagnostics.ProcessModule Module in HandleP[CurrProc].Modules)
            {
                if (Module.ModuleName.ToLower() == "game.dll")
                {
                    baseAddress = Module.BaseAddress.ToInt32();
                    return true;
                }
            }
            return false;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (PID != 0)
                {
                    hWnd = FindWindow(null, "AION Client");
                    if (hWnd != System.Diagnostics.Process.GetProcessById(PID).MainWindowHandle && !BindWindow)
                    {
                        System.Diagnostics.Process[] HandleP = System.Diagnostics.Process.GetProcessesByName("aion.bin");
                        int i;
                        for (i = 0; i < HandleP.Length; i++)
                        {
                            if (HandleP[i].MainWindowHandle == hWnd)
                            {
                                CurrProc = i;
                                initializeWindow();
                                return;
                            }

                        }
                        return;
                    }


                    pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;
                    if (PlayerOffset != 0)
                    {
                        PlayerTargId = ReadIntFromMemory(pHandle, PlayerOffset + offstarget_id);
                        PlayerId = ReadIntFromMemory(pHandle, baseAddress + offsplayerid);
                        if (PlayerTargId == PlayerId)
                        {
                            HP = ReadIntFromMemory(pHandle, baseAddress + offsetHP);
                            MP = ReadIntFromMemory(pHandle, baseAddress + offsetMP);
                            MaxHP = ReadIntFromMemory(pHandle, baseAddress + offsetMaxHP);
                            MaxMP = ReadIntFromMemory(pHandle, baseAddress + offsetMaxMP);
                            Fly = ReadIntFromMemory(pHandle, baseAddress + offscurrfly);
                            MaxFly = ReadIntFromMemory(pHandle, baseAddress + offsmaxfly);
                            GatherStatus = ReadByteFromMemory(pHandle, baseAddress + offstargetgathering);

                            Targ1 = ReadIntFromMemory(pHandle, baseAddress + offstarget1);
                            Targ2 = ReadIntFromMemory(pHandle, Targ1 + offstarget2);
                            TargetName = ReadStringFromMemory(pHandle, Targ2 + offstarget_name, 120);
                            TargetId = ReadIntFromMemory(pHandle, Targ2 + offstarget_id);
                            TargetType = ReadIntFromMemory(pHandle, Targ2 + offstarget_type);
                            TargetGatherable = ReadByteFromMemory(pHandle, Targ2 + offstarget_gatherable);
                            TargetHP = ReadIntFromMemory(pHandle, Targ2 + offstargetHP);
                            TargetMaxHP = ReadIntFromMemory(pHandle, Targ2 + offstargetMaxHP);
                            TargetLvl = ReadByteFromMemory(pHandle, Targ2 + offstargetLvl);
                            PlayerState = ReadIntFromMemory(pHandle, PlayerOffset + offstargetstate);
                            PlayerName = ReadStringFromMemory(pHandle, PlayerOffset + offstarget_name, 120);
                            PlayerCasting = ReadIntFromMemory(pHandle, PlayerOffset + offstargetcasting);
                            PlayerAtkSpeed = ReadIntFromMemory(pHandle, PlayerOffset + offsAtcSpeed);

                            GameTimer = ReadIntFromMemory(pHandle, baseAddress + 0xEFE290);

                            if (HP > 0 && MaxHP > 0)
                            {
                                float RezHP = ((float)HP / MaxHP) * 100;
                                if(RezHP <= 100)
                                    CHP = (int)Math.Round(RezHP);
                            }
                            if (MP > 0 && MaxMP > 0)
                            {
                                float RezMP = ((float)MP / MaxMP) * 100;
                                if(RezMP <= 100)
                                    CMP = (int)Math.Round(RezMP);
                            }
                            if (Fly > 0 && MaxFly > 0)
                            {
                                float RezFly = ((float)Fly / MaxFly) * 100;
                                if(RezFly <= 100)
                                    CFLY = (int)Math.Round(RezFly);
                            }
                            TargetDataSet(false);
                            if (!pause)
                            {
                                UsePots();
                                AutoCrab();
                            }
                                AttackSpeedModify();
                            if (!inv.Scanned)
                            {
                                inv.Scan(PID, baseAddress);
                            }
                        }
                        else
                        {
                            ResetData();
                            PlayerOffset = 0;
                            PlayerTargId = 0;
                            Status = false;
                            inv.Scanned = false;
                            if (autoTarget)
                            {
                                SendMessage(hWnd, 0x100, (int)Keys.F1, 0);
                                System.Threading.Thread.Sleep(500);
                            }
                        }
                    }
                    else
                    {
                        Targ1 = ReadIntFromMemory(pHandle, baseAddress + offstarget1);
                        Targ2 = ReadIntFromMemory(pHandle, Targ1 + offstarget2);
                        TargetId = ReadIntFromMemory(pHandle, Targ2 + offstarget_id);
                        PlayerId = ReadIntFromMemory(pHandle, baseAddress + offsplayerid);
                        MaxHP = ReadIntFromMemory(pHandle, baseAddress + offsetMaxHP);
                        if (PlayerId == TargetId)
                        {
                            PlayerOffset = Targ2;
                            PlayerTargId = TargetId;
                            Status = true;
                        }
                        else
                        {
                            if (MaxHP > 0)
                            {
                                ResetData();
                                PlayerOffset = 0;
                                PlayerTargId = 0;
                                PlayerAtkSpeed = 0;
                                inv.Scanned = false;
                                Status = false;
                                if (autoTarget)
                                {

                                    SendMessage(hWnd, 0x100, (int)Keys.F1, 0);
                                    System.Threading.Thread.Sleep(500);
                                }
                            }
                            else
                            {
                                ResetData();
                                PlayerOffset = 0;
                                PlayerTargId = 0;
                                PlayerAtkSpeed = 0;
                                inv.Scanned = false;
                                Status = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                updateTimer.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        public void TargetDataSet(bool Reset)
        {
            if (TargetId != 0 && !Reset)
            {
                TName = TargetName;
                THPmax = TargetMaxHP;
                THPmin = TargetHP;
                TLvl = TargetLvl;
            }
            else
            {
                TName = "НЕТ ЦЕЛИ";
                THPmax = 0;
                THPmin = 0;
                TLvl = 0;
            }
        }

        public void AttackSpeedModify()
        {
            int StaticSpeed;
            if (AtcSpeed && !pause)
            {
                StaticSpeed = ReadShortFromMemory(pHandle, baseAddress + offsStaticAtcSpeed);
                float NewSpeed = percAtkSpeed * StaticSpeed / 100;
                NewSpeed = (int)Math.Round(NewSpeed);
                if (PlayerAtkSpeed != NewSpeed)
                {
                    WriteShortMemory(pHandle, PlayerOffset + offsAtcSpeed, (int)NewSpeed);
                }
            }
            else
            {
                StaticSpeed = ReadIntFromMemory(pHandle, baseAddress + offsStaticAtcSpeed);
                if (PlayerAtkSpeed != StaticSpeed)
                {
                    WriteShortMemory(pHandle, PlayerOffset + offsAtcSpeed, StaticSpeed);
                }
            }
        }

        public void BBind(bool stat)
        {
            if (gkh.HookedKeys.Count == 0)
            {
                gkh.HookedKeys.Add(Keys.NumPad5);
                gkh.HookedKeys.Add(Keys.NumPad0);
                gkh.HookedKeys.Add(Keys.NumPad6);
                gkh.HookedKeys.Add(Keys.NumPad2);
                gkh.HookedKeys.Add(Keys.NumPad4);
                gkh.HookedKeys.Add(Keys.NumPad8);
                gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            }
            if (stat)
            {
                gkh.hook();
            }
            else
            {
                gkh.unhook();
            }
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pause)
            {
                try
                {
                    switch (e.KeyCode)
                    {
                        case (Keys.NumPad5):
                            if (!NoGrav)
                            {
                                onMessage("НОГРАВИТИ ВКЛЮЧЕНО");
                                (new Thread(new ThreadStart(RealizeNoGrav))).Start();
                            }
                            else
                            {
                                onMessage("НОГРАВИТИ ВЫКЛЮЧЕНО");
                                NoGrav = false;
                            }
                            break;
                        case (Keys.NumPad0):
                            RealizeFly(7, true);
                            break;
                        case (Keys.NumPad6):
                            RealizeTP(1, true, true);
                            break;
                        case (Keys.NumPad4):
                            RealizeTP(1, false, true);
                            break;
                        case (Keys.NumPad8):
                            RealizeTP(1, true, false);
                            break;
                        case (Keys.NumPad2):
                            RealizeTP(1, false, false);
                            break;
                    }
                }
                catch
                {
                    e.Handled = true;
                }
            }
            else
            {
                onMessage("Я НА ПАУЗЕ - ИГНОР КНОПОК");
                e.Handled = false;
            }
        }

        private void RealizeFly(int f_len, bool dir)
        {
            pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;
            if (dir)
            {
                WriteFloatMemory(pHandle, PlayerOffset + offsFly1, (float)f_len);
            }
            else
            {
                WriteFloatMemory(pHandle, PlayerOffset + offsFly1, (-1) * (float)f_len);
                WriteFloatMemory(pHandle, PlayerOffset + offsFly2, 0);
            }
        }

        public void RealizeTP(int jmp_l, bool dir, bool mode)
        {
            pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;
            int CoordsStruct = ReadIntFromMemory(pHandle, PlayerOffset + 4);
            CoordsStruct = ReadIntFromMemory(pHandle, CoordsStruct + offsplayercoords);
            float CoordZ = ReadFloatFromMemory(pHandle, CoordsStruct + offsplayercoordsz, 4);
            float CoordX = ReadFloatFromMemory(pHandle, CoordsStruct + offsplayercoordsx, 4);
            float CoordY = ReadFloatFromMemory(pHandle, CoordsStruct + offsplayercoordsy, 4);
            float Rot = ReadFloatFromMemory(pHandle, baseAddress + offsplayerrotation, 4);
            if (mode)
            {
                if (Rot < 0)
                {
                    Rot = Math.Abs(Rot);
                    if (Rot >= 90)
                    {
                        Rot = Rot - 90;
                        float CoordAddY = jmp_l * (float)Math.Sin(Math.PI * Rot / 180);
                        float CoordAddX = jmp_l * (float)Math.Sin(Math.PI * (90 - Rot) / 180);
                        if (dir)
                        {
                            CoordX -= CoordAddX;
                            CoordY += CoordAddY;
                        }
                        else
                        {
                            CoordX += CoordAddX;
                            CoordY -= CoordAddY;
                        }
                    }
                    else
                    {
                        float CoordAddY = jmp_l * (float)Math.Sin(Math.PI * Rot / 180);
                        float CoordAddX = jmp_l * (float)Math.Sin(Math.PI * (90 - Rot) / 180);
                        if (dir)
                        {
                            CoordX -= CoordAddY;
                            CoordY -= CoordAddX;
                        }
                        else
                        {
                            CoordX += CoordAddY;
                            CoordY += CoordAddX;
                        }
                    }
                }
                else
                {
                    if (Rot >= 90)
                    {
                        Rot = Rot - 90;
                        float CoordAddY = jmp_l * (float)Math.Sin(Math.PI * Rot / 180);
                        float CoordAddX = jmp_l * (float)Math.Sin(Math.PI * (90 - Rot) / 180);
                        if (dir)
                        {
                            CoordX += CoordAddX;
                            CoordY += CoordAddY;
                        }
                        else
                        {
                            CoordX -= CoordAddX;
                            CoordY -= CoordAddY;
                        }
                    }
                    else
                    {
                        float CoordAddY = jmp_l * (float)Math.Sin(Math.PI * Rot / 180);
                        float CoordAddX = jmp_l * (float)Math.Sin(Math.PI * (90 - Rot) / 180);
                        if (dir)
                        {
                            CoordX += CoordAddY;
                            CoordY -= CoordAddX;
                        }
                        else
                        {
                            CoordX -= CoordAddY;
                            CoordY += CoordAddX;
                        }
                    }
                }
                WriteFloatMemory(pHandle, CoordsStruct + offsplayercoordsx, CoordX);
                WriteFloatMemory(pHandle, CoordsStruct + offsplayercoordsy, CoordY);
            }
            else
            {
                if (dir)
                {
                    CoordZ += jmp_l;
                }
                else
                {
                    CoordZ -= jmp_l;
                }
                WriteFloatMemory(pHandle, CoordsStruct + offsplayercoordsz, CoordZ);
            }
        }

        private void RealizeNoGrav()
        {
            try
            {
                pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;
                int PlayerState = ReadByteFromMemory(pHandle, PlayerOffset + 0x890);
                if (!NoGrav)
                {
                    NoGrav = true;
                    while (NoGrav)
                    {
                        //if (PlayerState != 7)
                        if (PlayerState == 0)
                        {
                            WriteByteMemory(pHandle, PlayerOffset + 0x1698, 0);
                            WriteByteMemory(pHandle, PlayerOffset + 0x1AF0, 0);
                            WriteByteMemory(pHandle, PlayerOffset + 0x1AF8, 0);
                            WriteByteMemory(pHandle, PlayerOffset + 0x898, 5);
                        }
                        else
                        {
                            PlayerState = ReadByteFromMemory(pHandle, PlayerOffset + 0x898);
                        }
                        System.Threading.Thread.Sleep(10);
                    }
                    WriteByteMemory(pHandle, PlayerOffset + 0x898, 0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Упс! Поймал эксцепшн в блоке НоГравити! " + e.Message);
            }
        }


        public Int32 ReadIntFromMemory(IntPtr handle, int address)
        {
            byte[] buffer = new byte[4];
            IntPtr read = IntPtr.Zero;
            ReadProcessMemory(handle, (IntPtr)address, buffer, 4, out read);
            return (int)BitConverter.ToUInt32(buffer, 0);
        }

        private Int32 ReadShortFromMemory(IntPtr handle, int address)
        {
            byte[] buffer = new byte[2];
            IntPtr read = IntPtr.Zero;
            ReadProcessMemory(handle, (IntPtr)address, buffer, 2, out read);
            return (int)BitConverter.ToInt16(buffer, 0);
        }

        private byte ReadByteFromMemory(IntPtr handle, int address)
        {
            byte[] buffer = new byte[1];
            IntPtr read = IntPtr.Zero;
            ReadProcessMemory(handle, (IntPtr)address, buffer, 1, out read);
            return buffer[0];
        }

        public String ReadStringFromMemory(IntPtr handle, int address, int length)
        {
            IntPtr read = IntPtr.Zero;
            string rtnStr = string.Empty;
            byte[] buffer = new byte[length];

            ReadProcessMemory(handle, (IntPtr)address, buffer, (uint)length, out read);

            UnicodeEncoding enc = new UnicodeEncoding();
            rtnStr = enc.GetString(buffer);
            if (rtnStr.IndexOf('\0') >= 0)
                return rtnStr.Substring(0, rtnStr.IndexOf('\0'));
            else
                return "";
        }

        private Single ReadFloatFromMemory(IntPtr handle, int address, int length)
        {
            IntPtr read = IntPtr.Zero;
            byte[] buffer = new byte[length];

            ReadProcessMemory(handle, (IntPtr)address, buffer, (uint)length, out read);

            return BitConverter.ToSingle(buffer, 0);
        }

        public static bool WriteFloatMemory(IntPtr pHandle, int Address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteProcessMemory(pHandle, (IntPtr)Address, buffer, (uint)buffer.Length, IntPtr.Zero);
        }

        public static bool WriteIntMemory(IntPtr pHandle, int Address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteProcessMemory(pHandle, (IntPtr)Address, buffer, 4, IntPtr.Zero);
        }

        public static bool WriteShortMemory(IntPtr pHandle, int Address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteProcessMemory(pHandle, (IntPtr)Address, buffer, 2, IntPtr.Zero);
        }

        public static bool WriteByteMemory(IntPtr pHandle, int Address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return WriteProcessMemory(pHandle, (IntPtr)Address, buffer, 1, IntPtr.Zero);
        }

        private void UsePots()
        {
            if (startHPMP)
            {
                float CurrPercHP = 100 * (float)HP / MaxHP;
                CurrPercHP = (int)Math.Round(CurrPercHP);
                float CurrPercMP = 100 * (float)MP / MaxMP;
                CurrPercMP = (int)Math.Round(CurrPercMP);
                switch (Priority)
                {
                    case (0):
                        if (CurrPercMP > CurrPercHP - 2)
                        {
                            if (CurrPercHP < percHP && CurrPercHP != 0)
                                SendPotButton("hp");
                        }
                        else
                        {
                            if (CurrPercMP < percMP && CurrPercHP != 0)
                                SendPotButton("mp");
                        }
                        break;
                    case (1):
                        if (CurrPercHP < percHP && CurrPercHP != 0)
                            SendPotButton("hp");
                        if (CurrPercMP < percMP && CurrPercHP >= percHP && CurrPercHP != 0)
                            SendPotButton("mp");
                        break;
                    case (2):
                        if (CurrPercMP < percMP && CurrPercHP != 0)
                            SendPotButton("mp");
                        if (CurrPercHP < percHP && CurrPercMP >= percMP && CurrPercHP != 0)
                            SendPotButton("hp");
                        break;
                }
            }
            if (startFLY)
            {
                if (GatherStatus == 0 && PlayerState == 4)
                {
                    if (MaxFly - Fly >= flyVal)
                        SendPotButton("fly");
                }
            }
        }
        private void AutoCrab()
        {
            if (autoCrb && TargetType == 7 && GatherStatus == 0)
            {
                if (Environment.TickCount - 300 >= GathStart)
                {
                    GathStart = Environment.TickCount;
                    SendMessage(hWnd, 0x100, (int)Keys.D1, 0);
                    System.Threading.Thread.Sleep(100);
                    SendMessage(hWnd, 0x101, (int)Keys.D1, 0);
                    System.Threading.Thread.Sleep(200);
                }
                if (TargetId != 0)
                {
                    if (TargetGatherable > 9)
                    {
                        TargetName = "НАЙДЕН РЕСУРС: " + TargetName;
                    }
                    else
                    {
                        TargetName = "НЕ МОГУ СОБРАТЬ: " + TargetName;
                    }
                }
            }
        }

        private void SendPotButton(string mode)
        {
            int Key = 0;
            if (inv.Scanned)
            {
                switch (mode)
                {
                    case ("hp"):
                        if (CheckPotTimeout(hppot) && CheckCasting())
                            onMessage("ЖРУ ХИЛКУ");
                        Key = (int)Keys.F11;
                        break;
                    case ("mp"):
                        if (CheckPotTimeout(mppot) && CheckCasting())
                            onMessage("ЖРУ МАНУ");
                        Key = (int)Keys.F12;
                        break;
                    case ("fly"):
                        if (CheckPotTimeout(flpot))
                            onMessage("ЖРУ НА ПОЛЕТ");
                        Key = (int)Keys.F9;
                        break;
                }
                if (Key != 0)
                {
                    SendMessage(hWnd, 0x100, Key, 0);
                    System.Threading.Thread.Sleep(100);
                    SendMessage(hWnd, 0x101, Key, 0);
                }
            }
            else
            {
                onMessage("НЕ МОГУ СОЖРАТЬ БАНКУ, ИНВЕНТАРЬ НЕ ПЕРЕСКАНИРОВАН");
            }
        }

        private bool CheckPotTimeout(uint id)
        {
            if (inv.Items.ContainsKey(id))
            {
                int offs = inv.Items[id].ItemOffset;
                offs = (int)(offs + 0x40);
                uint ntime = (uint)ReadIntFromMemory(pHandle, offs);
                if (GameTimer > ntime)
                    return true;
                else
                    return false;
            }
            else
            {
                //MessageBox.Show("ГДЕ-ТО НЕ ВЫБРАНА БАНКА! БУДУ СПАМИТЬ КНОПКОЙ!");
                return true;
            }
        }

        private bool CheckCasting()
        {
            if (ReadIntFromMemory(pHandle, PlayerOffset + offstargetcasting) == 0)
               return true;
            else
               return false;
        }
    }
}
