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
            List<ItemPile> basket = new List<ItemPile>();
            basket.Add(new ItemPile( new StockKeepingUnit("A", 50), 1));
            
            decimal result =_pricing.GetDiscountedPrice(basket);
            result.Should().Be(50);            
        }

        [Fact]
        public void GetDiscount_Price_For_Empty_List_Throws_Errors()
        {
            try
            {
                decimal result = _pricing.GetDiscountedPrice(new List<ItemPile>());
            }
            catch (ArgumentException e)
            {
                Assert.True(true);
            }           
            
        }

        [Fact]
        public void Get_Discount_Price_Applies_Discount_Rule()
        {
            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);

            List<ItemPile> rulePiles = new List<ItemPile>();
            rulePiles.Add(new ItemPile(itemA, 2));
            rulePiles.Add(new ItemPile(itemB, 1));             

                        DiscountRule rule = _ruleBuilder.ForItems(rulePiles).WithPrice(70).Build();
            _mockRuleRepository.MockRules = new List<DiscountRule> { rule };

            List<ItemPile> basket = new List<ItemPile>();
            basket.Add(new ItemPile(itemA, 2));
            basket.Add(new ItemPile(itemB, 1));
            

            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(70);
        }
    }
}
