using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Stock
{
    public class StockKeepingUnit
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public StockKeepingUnit(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
