﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Stock;

namespace Store.Pricing
{
    public interface IRuleRepository
    {
        IEnumerable<DiscountRule> GetRules();        
    }
}
