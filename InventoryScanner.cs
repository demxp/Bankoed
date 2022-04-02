using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BANCOED
{
    public class InventoryScanner
    {
        private IntPtr pHandle = IntPtr.Zero;
        private int baseAddress = 0;
        public Dictionary<uint, Item> Items;
        private HashSet<int> found;
        public int PID;
        public bool Scanned = false;
        private Memory mem;

        public delegate void InvScan();
        public event InvScan onScanned;

        private void Recurse(Item It)
        {
            if (!found.Add(It.PtrBase))
                return;

            if (It.ItemName != "")
            {
                Items.Add(It.ItemId, It);
            }
            if (!found.Contains(It.PtrChild1))
                Recurse(new Item(It.PtrChild1, PID));
            if (!found.Contains(It.PtrChild3))
                Recurse(new Item(It.PtrChild3, PID));
            if (!found.Contains(It.PtrChild2))
                Recurse(new Item(It.PtrChild2, PID));
        }

        private void MatchHotbar()
        {
            for (int bars = 0; bars <= 12; bars++)
            {
                int thisbar = bars * 0xF0;
                for (int i = 0; i < 12; i++)
                {
                    long caddress = baseAddress + 0xF40FE8 + thisbar + (i * 0x14);
                    int itemtype = (int)mem.ReadIntFromMemory(pHandle, (int)(caddress));
                    uint itemid = (uint)mem.ReadIntFromMemory(pHandle, (int)(caddress + 0x8));
                    if (itemtype == 1 && Items.ContainsKey(itemid))
                    {
                            Items[itemid].InHotBar = true;
                    }
                }
            }
        }

        public void Scan(int PID, int baseAddr)
        {
            found = new HashSet<int>();
            Items = new Dictionary<uint, Item>();
            mem = new Memory();

            this.PID = PID;
            this.baseAddress = baseAddr;
            this.pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;

            int Start = baseAddress + 0xED31AC;

            Start = (int)mem.ReadIntFromMemory(pHandle, (int)(Start));
            Start = (int)mem.ReadIntFromMemory(pHandle, (int)(Start + 0x97C));
            Start = (int)mem.ReadIntFromMemory(pHandle, (int)(Start + 0x18));

            Recurse(new Item(Start, PID));
            MatchHotbar();
            Scanned = true;
            onScanned();
        }
    }



    public class Item
    {
        public int PtrBase;

        public IntPtr pHandle;

        public int PtrChild1 { get; set; }
        public int PtrChild2 { get; set; }
        public int PtrChild3 { get; set; }

        public int ItemCount;
        public string ItemName;
        public uint ItemId;
        public int ItemOffset;
        public int StartTime;
        public int EndTime;
        public bool InHotBar = false;

        public Item(int ptr, int PID)
        {
            this.PtrBase = ptr;
            this.pHandle = System.Diagnostics.Process.GetProcessById(PID).Handle;
            SetZero();
            this.Update();
        }

        public override string ToString()
        {
            return this.ItemName;
        }

        Memory mem = new Memory();

        public void Update()
        {
            if (PtrBase != 0)
            {
                PtrChild1 = (int)mem.ReadIntFromMemory(pHandle, (int)PtrBase);
                PtrChild2 = (int)mem.ReadIntFromMemory(pHandle, (int)(PtrBase + 0x04));
                PtrChild3 = (int)mem.ReadIntFromMemory(pHandle, (int)(PtrBase + 0x08));

                int ItemAddr = (int)mem.ReadIntFromMemory(pHandle, (int)PtrBase + 0x10);

                ItemOffset = ItemAddr;
                int ItemNameLink = (int)mem.ReadIntFromMemory(pHandle, (int)(ItemAddr + 0x1C));
                StartTime = (int)mem.ReadIntFromMemory(pHandle, (int)(ItemAddr + 0x38));
                EndTime = (int)mem.ReadIntFromMemory(pHandle, (int)(ItemAddr + 0x40));
                ItemCount = (int)mem.ReadIntFromMemory(pHandle, (int)(ItemAddr + 0x10));
                ItemName = mem.ReadStringFromMemory(pHandle, (int)(ItemNameLink), 80);
                ItemId = (uint)mem.ReadIntFromMemory(pHandle, (int)(ItemAddr + 0x8));
            }
        }

        public void SetZero()
        {
            ItemName = "";
            ItemCount = 0;
            ItemId = 0;
        }
    }
}
