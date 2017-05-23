using Store.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Pricing
{
    public class DiscountUnit
    {
        public StockKeepingUnit Item { get; set; }
        public int Quantity { get; set; }
    }
}
