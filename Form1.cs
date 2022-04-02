using System;
using System.Drawing;
using System.Windows.Forms;

namespace BANCOED
{
    public partial class Form1 : Form
    {
        public Memory mem;
        public System.Windows.Forms.Timer Upd;
        public bool FormStat, Memstat, Shlog, KbHook, potsscanned;
        public uint currhppot, currmppot, currflypot, playerid;
        private uint CHPpot, CMPpot, CFLYpot;
        Form3 form3 = new Form3();
        public Form1()
        {
            InitializeComponent();
            FormStat = false;
            Memstat = false;
            Shlog = true;
            KbHook = false;
            potsscanned = false;
            CHPpot = 0;
            CMPpot = 0;
            CFLYpot = 0;
            mem = new Memory();
            Upd = new System.Windows.Forms.Timer();
            Upd.Interval = 200;
            Upd.Tick += new EventHandler(UpdTimerTick);
            Upd.Enabled = true;
            this.mpperc.SelectedIndex = 5;
            this.hpperc.SelectedIndex = 5;
            this.atcSpeed.SelectedIndex = 6;
            mem.onMessage += this.ShowTrayMessage;
            mem.inv.onScanned += this.InventoryRescan;
        }

        public void UpdTimerTick(object sender, EventArgs e)
        {
            if (!Memstat)
            {
                Memstat = mem.Monitor();
            }
            else
            {
                if (mem.Status)
                {
                    this.colorProgressBar1.Value = mem.CHP;
                    this.colorProgressBar2.Value = mem.CMP;
                    this.colorProgressBar3.Value = mem.CFLY;
                    this.currAtkSpeed.Text = mem.PlayerAtkSpeed.ToString();
                    this.Text = mem.PlayerName;
                    this.label1.Text = mem.TargetName;
                    this.form3.tname.Text = mem.TName+" ("+mem.TLvl.ToString()+")";
                    this.form3.targethp.Text = mem.THPmin.ToString() + "/" + mem.THPmax.ToString();
                    if (mem.THPmin > 0 && mem.THPmax > 0)
                    {
                        this.form3.targHPperc.Value = (int)(Math.Round((decimal)mem.THPmin * 100 / (decimal)mem.THPmax));
                    }
                    else
                    {
                        this.form3.targHPperc.Value = 0;
                    }
                    if (!mem.startFLY)
                    {
                        this.button4.Text = "ВКЛЮЧИТЬ";
                        this.button4.BackColor = Color.FromArgb(192, 192, 255);
                    }
                    if (!mem.startHPMP)
                    {
                        this.button3.Text = "ВКЛЮЧИТЬ";
                        this.button3.BackColor = Color.FromArgb(192, 192, 255);
                    }
                    if (!mem.AtcSpeed)
                    {
                        this.button5.Text = "ВКЛЮЧИТЬ";
                        this.button5.BackColor = Color.FromArgb(192, 192, 255);
                    }
                    if (Shlog)
                    {
                        //this.textBox1.Text = mem.Log;
                    }
                }
                else
                {
                    this.colorProgressBar1.Value = 0;
                    this.colorProgressBar2.Value = 0;
                    this.colorProgressBar3.Value = 0;
                    this.currAtkSpeed.Text = "";
                    this.Text = "ОШИБКА ВЫБОРА ПЕРСОНАЖА";
                    this.label1.Text = mem.TargetName;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!FormStat)
            {
                FormStat = true;
                this.TopMost = true;
                this.Opacity = 0.6;
                this.button2.Text = "ОБЫЧНЫЙ РЕЖИМ";
            }
            else
            {
                FormStat = false;
                this.TopMost = false;
                this.Opacity = 1;
                this.button2.Text = "ПОВЕРХ ОКНА ИГРЫ";
            }
        }

        private void hpperc_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.hpperc.SelectedIndex)
            {
                case(0):
                    mem.percHP = 10;
                    break;
                case(1):
                    mem.percHP = 20;
                    break;
                case(2):
                    mem.percHP = 30;
                    break;
                case(3):
                    mem.percHP = 40;
                    break;
                case(4):
                    mem.percHP = 50;
                    break;
                case(5):
                    mem.percHP = 60;
                    break;
                case(6):
                    mem.percHP = 70;
                    break;
                case(7):
                    mem.percHP = 80;
                    break;
                case(8):
                    mem.percHP = 90;
                    break;
                case(9):
                    mem.percHP = 95;
                    break;
            }
        }

        private void mpperc_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.mpperc.SelectedIndex)
            {
                case (0):
                    mem.percMP = 10;
                    break;
                case (1):
                    mem.percMP = 20;
                    break;
                case (2):
                    mem.percMP = 30;
                    break;
                case (3):
                    mem.percMP = 40;
                    break;
                case (4):
                    mem.percMP = 50;
                    break;
                case (5):
                    mem.percMP = 60;
                    break;
                case (6):
                    mem.percMP = 70;
                    break;
                case (7):
                    mem.percMP = 80;
                    break;
                case (8):
                    mem.percMP = 90;
                    break;
                case (9):
                    mem.percMP = 95;
                    break;
            }
        }

        private void flypot_SelectedIndexChanged(object sender, EventArgs e)
        {
            mem.flpot = ((Item)this.flypot.SelectedItem).ItemId;
            CFLYpot = ((Item)this.flypot.SelectedItem).ItemId;
            switch (((Item)this.flypot.SelectedItem).ItemName)
            {
                case ("Редкое зелье ветра I"):
                    mem.flyVal = 12000;
                    break;
                case ("Редкое зелье ветра II"):
                    mem.flyVal = 24000;
                    break;
                case ("Редкое зелье ветра III"):
                    mem.flyVal = 36000;
                    break;
                case ("Редкое зелье ветра IV"):
                    mem.flyVal = 48000;
                    break;
                case ("Редкое зелье ветра V"):
                    mem.flyVal = 60000;
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mem.autoCrb = this.checkBox1.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mem.startHPMP)
            {
                mem.startHPMP = false;
                this.button3.Text = "ВКЛЮЧИТЬ";
                this.button3.BackColor = Color.FromArgb(192, 192, 255);
            }
            else
            {
                mem.startHPMP = true;
                this.button3.Text = "ВЫКЛЮЧИТЬ";
                this.button3.BackColor = Color.FromArgb(192, 255, 192);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mem.startFLY)
            {
                mem.startFLY = false;
                this.button4.Text = "ВКЛЮЧИТЬ";
                this.button4.BackColor = Color.FromArgb(192, 192, 255);
            }
            else
            {
                mem.startFLY = true;
                this.button4.Text = "ВЫКЛЮЧИТЬ";
                this.button4.BackColor = Color.FromArgb(192, 255, 192);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mem.Priority = 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mem.Priority = 2;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mem.Priority = 1;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (Shlog)
            {
                Shlog = false;
            }
            else
            {
                Shlog = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!KbHook)
            {
                this.button8.BackColor = Color.FromArgb(192, 255, 192);
                mem.BBind(true);
                KbHook = true;
                Console.WriteLine("Гор кл вкл");
            }
            else
            {
                this.button8.BackColor = Color.FromArgb(192, 192, 255);
                mem.BBind(false);
                KbHook = false;
                Console.WriteLine("Гор кл выкл");
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            mem.autoTarget = this.checkBox2.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.atcSpeed.SelectedIndex)
            {
                case (0):
                    mem.percAtkSpeed = 10;
                    break;
                case (1):
                    mem.percAtkSpeed = 20;
                    break;
                case (2):
                    mem.percAtkSpeed = 30;
                    break;
                case (3):
                    mem.percAtkSpeed = 40;
                    break;
                case (4):
                    mem.percAtkSpeed = 50;
                    break;
                case (5):
                    mem.percAtkSpeed = 60;
                    break;
                case (6):
                    mem.percAtkSpeed = 70;
                    break;
                case (7):
                    mem.percAtkSpeed = 80;
                    break;
                case (8):
                    mem.percAtkSpeed = 90;
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!mem.AtcSpeed)
            {
                mem.AtcSpeed = true;
                this.button5.Text = "ВЫКЛЮЧИТЬ";
                this.button5.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                this.button5.Text = "ВКЛЮЧИТЬ";
                this.button5.BackColor = Color.FromArgb(192, 192, 255);
                mem.AtcSpeed = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            mem.BindWindow = this.checkBox3.Checked;
        }

        private void healpotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            mem.hppot = ((Item)this.healpotion.SelectedItem).ItemId;
            CHPpot = ((Item)this.healpotion.SelectedItem).ItemId;
        }

        private void manapotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            mem.mppot = ((Item)this.manapotion.SelectedItem).ItemId;
            CMPpot = ((Item)this.manapotion.SelectedItem).ItemId;
        }

        private void hpform_CheckedChanged(object sender, EventArgs e)
        {
            if (hpform.Checked == true)
                form3.Show(this);
            else
                form3.Hide();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if(this.hpform.Checked)
                    form3.Show();
                notifyIcon1.BalloonTipText = "БАНКОЕД СВЕРНУТ";
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mem.pause)
            {
                mem.pause = false;
                notifyIcon1.BalloonTipText = "БАНКОЕД ВКЛЮЧЕН";
                notifyIcon1.ShowBalloonTip(500);
            }
            else
            {
                mem.pause = true;
                notifyIcon1.BalloonTipText = "БАНКОЕД ПАУЗА";
                notifyIcon1.ShowBalloonTip(500);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            mem.NoGrav = false;
            mem.pause = true;
            System.Threading.Thread.Sleep(100);
            Application.Exit();
        }

        private void ShowTrayMessage(string Mess)
        {
            if (Mess.Length > 0)
            {
                notifyIcon1.BalloonTipText = Mess;
                notifyIcon1.ShowBalloonTip(500);
            }
        }

        private void InventoryRescan()
        {
            if (this.playerid == mem.PlayerId)
            {
                mem.flpot = this.CFLYpot;
                mem.hppot = this.CHPpot;
                mem.mppot = this.CMPpot;
            }
            else
            {
                this.healpotion.Items.Clear();
                this.manapotion.Items.Clear();
                this.flypot.Items.Clear();
                foreach (var Item in mem.inv.Items)
                {
                    if (Item.Value.InHotBar)
                    {
                        this.healpotion.Items.Add(Item.Value);
                        this.manapotion.Items.Add(Item.Value);
                        this.flypot.Items.Add(Item.Value);
                    }
                }
            }
            this.playerid = (uint)mem.PlayerId;
        }
    }
}
