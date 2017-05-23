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
        
        private List<DiscountRule> _mockRules = new List<DiscountRule>();

        public MockRuleRepository()
        {
           
        }

        public IEnumerable<DiscountRule> GetRules()
        {
            throw new NotImplementedException();
        }
    }
}
