using Store.Pricing;
using Store.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Test
{
    public class MockRuleRepository : IRuleRepository
    {   
        public MockRuleRepository()
        {
            MockRules = new List<DiscountRule>();
        }

        public IEnumerable<DiscountRule> GetRules()
        {
            return MockRules;
        }

        public IEnumerable<DiscountRule> GetRulesForItems(List<StockKeepingUnit> items)
        {
            throw new NotImplementedException();
        }

        public List<DiscountRule> MockRules { get; set; }   
    }
}
