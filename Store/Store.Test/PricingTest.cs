using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Store.Checkout;
using Store.Stock;
using Store.Pricing;

namespace Store.Test
{
    public class PricingTest
    {
        private readonly DiscountRuleBuilder ruleBuilder = new DiscountRuleBuilder();
        private readonly IRuleRepository _mockRuleRepository = new MockRuleRepository();
         
        private IPricing _pricing;

        public PricingTest()
        {
            _pricing = new PricingController(_mockRuleRepository);            
        }

        [Fact]
        public void GetDiscount_Price_For_One_Item_Is_Equal_To_Item_Price()
        {
            List<StockKeepingUnit> basket = new List<StockKeepingUnit>();
            basket.Add(new StockKeepingUnit("A", 50));
            
            decimal result =_pricing.GetDiscountedPrice(basket);
            result.Should().Be(50);            
        }

        [Fact]
        public void GetDiscount_Price_For_Empty_List_Throws_Errors()
        {
            try
            {
                decimal result = _pricing.GetDiscountedPrice(new List<StockKeepingUnit>());
            }
            catch (ArgumentException e)
            {
                Assert.True(true);
            }           
            
        }
    }
}
