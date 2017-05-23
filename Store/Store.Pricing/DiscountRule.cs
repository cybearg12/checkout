using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Pricing
{
    public class DiscountRule
    {
        public List<DiscountUnit> Items { get; set; }
        public decimal Price { get; set; }
    }
}
