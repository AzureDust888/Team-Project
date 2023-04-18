using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    public class Item
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double sell_price { get; set; } = 0.0;

        public Item() { }

        public Item(string name, string description, double sell_price)
        {
            Name = name;
            Description = description;
            this.sell_price = sell_price;
        }
    }
}
