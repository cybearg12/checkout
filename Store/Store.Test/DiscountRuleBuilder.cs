﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Pricing;
using Store.Stock;

namespace Store.Test
{
    public class DiscountRuleBuilder
    {
        private List<DiscountGroup> _discountGroups;
        private decimal _discountPrice;

        public void ForItems(List<StockKeepingUnit> items)
        {
            _discountGroups = new List<DiscountGroup>();
            foreach (var itemGroup in items.GroupBy(i => i.Name))
            {
                DiscountGroup gr = new DiscountGroup { Item = itemGroup.First(), Quantity = itemGroup.Count() };
                _discountGroups.Add(gr);
            }
        }

        public void WithPrice(decimal price)
        {
            _discountPrice = price;
        }
              

        public DiscountRule Build()
        {
            DiscountRule result = new DiscountRule
            {
                Items = _discountGroups,
                Price = _discountPrice
            };

            return result;
        }
    }
}
