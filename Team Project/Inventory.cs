using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    public class Inventory
    {
        public int Capacity { get; set; } = 10;
        public int MaxCapacity_Slot { get; set; } = 20;
        public List<Item> Items { get; set; } = new List<Item>();

        public Inventory() { }
    }
}
