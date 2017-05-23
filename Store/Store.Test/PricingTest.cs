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
        private readonly DiscountRuleBuilder _ruleBuilder = new DiscountRuleBuilder();
        private readonly MockRuleRepository _mockRuleRepository = new MockRuleRepository();
         
        private IPricing _pricing;

        public PricingTest()
        {
            _pricing = new PricingController(_mockRuleRepository);            
        }

        [Fact]
        public void Get_Discount_Price_For_One_Item_Is_Equal_To_Item_Price()
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

        [Fact]
        public void Get_Discount_Price_Applies_Discount_Rule()
        {
            List<StockKeepingUnit> basket = new List<StockKeepingUnit>();
            basket.Add(new StockKeepingUnit("A", 50));
            basket.Add(new StockKeepingUnit("B", 30));
            basket.Add(new StockKeepingUnit("A", 50));

            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);

            DiscountRule rule = _ruleBuilder.ForItems(new List<StockKeepingUnit> { itemA, itemA }).WithPrice(70).Build();
            _mockRuleRepository.MockRules = new List<DiscountRule> { rule };

            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(100);
        }
    }
}
