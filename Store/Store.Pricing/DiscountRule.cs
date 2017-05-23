﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Stock;

namespace Store.Pricing
{
    public class DiscountRule
    {
        public List<DiscountGroup> Items { get; set; }
        public decimal Price { get; set; }

        public DiscountRule()
        {
            Items = new List<DiscountGroup>();
        }                
    }
}
