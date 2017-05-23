﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Stock;

namespace Store.Pricing
{
    public class PricingController : IPricing
    {
        private readonly IRuleRepository _ruleRepository;

        public PricingController(IRuleRepository repository)
        {
            _ruleRepository = repository;
        }

        public decimal GetDiscountedPrice(List<ItemPile> itemPiles)
        {
            if (itemPiles == null || itemPiles.Count == 0)
                throw new ArgumentException("Items collection is empty");

            return itemPiles.Sum(p => p.Price);            
        }
    }
}
