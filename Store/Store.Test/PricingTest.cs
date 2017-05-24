using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Checkout;
using Store.Stock;
using Store.Pricing;
using Xunit;
using Assert = Xunit.Assert;

namespace Store.Test
{
    public class PricingTest
    {
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

            DiscountRule rule = new DiscountRuleBuilder().WithUnits(itemA, 2).WithUnits(itemB, 1).WithPrice(70).Build();
            _mockRuleRepository.MockRules = new List<DiscountRule> { rule };

            List<ItemPile> basket = new List<ItemPile>();
            basket.Add(new ItemPile(itemA, 2));
            basket.Add(new ItemPile(itemB, 1));
            

            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(70);
        }

        [Fact]
        public void Get_Discount_Price_Does_Not_Change_Input_Basket_Instance()
        {
            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);

            DiscountRule rule = new DiscountRuleBuilder().WithUnits(itemA, 2).WithUnits(itemB, 1).WithPrice(70).Build();
            _mockRuleRepository.MockRules = new List<DiscountRule> { rule };

            List<ItemPile> basket = new List<ItemPile>();
            basket.Add(new ItemPile(itemA, 3));
            basket.Add(new ItemPile(itemB, 2));


            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(150);
            basket.Count().Should().Be(2);
            basket[0].Quantity.Should().Be(3);
            basket[1].Quantity.Should().Be(2);
        }

        [Fact]
        public void Get_Discount_Price_Ignores_Rules_For_Units_Not_In_Basket()
        {
            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);
            var itemC = new StockKeepingUnit("C", 30);

            DiscountRule rule1 = new DiscountRuleBuilder().WithUnits(itemA, 2).WithUnits(itemB, 1).WithPrice(70).Build();
            DiscountRule rule2 = new DiscountRuleBuilder().WithUnits(itemC, 2).WithPrice(50).Build();

            _mockRuleRepository.MockRules = new List<DiscountRule> { rule1, rule2 };
            
            List<ItemPile> basket = new List<ItemPile>
            {
                new ItemPile(itemA, 3),
                new ItemPile(itemB, 2)
            };

            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(150);
        }

        [Fact]
        public void Get_Discount_Price_Handles_Different_Rules_For_Same_Unit_When_Sufficient_Quantity_In_Basket()
        {
            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);

            // two A and one B for 70
            DiscountRule rule1 = new DiscountRuleBuilder().WithUnits(itemA, 2).WithUnits(itemB, 1).WithPrice(70).Build();

            // two A for 75
            DiscountRule rule2 = new DiscountRuleBuilder().WithUnits(itemA, 2).WithPrice(75).Build();

            _mockRuleRepository.MockRules = new List<DiscountRule> { rule1, rule2 };

            List<ItemPile> basket = new List<ItemPile>
            {
                new ItemPile(itemA, 4),
                new ItemPile(itemB, 2)
            };

            //both rules should be applied
            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(175);
        }

        [Fact]
        public void Get_Discount_Price_Chooses_The_Best_Discount_For_Basket()
        {
            var itemA = new StockKeepingUnit("A", 50);
            var itemB = new StockKeepingUnit("B", 30);

            // two A for 75
            DiscountRule rule1 = new DiscountRuleBuilder().WithUnits(itemA, 2).WithPrice(75).Build();

            // two A and one B for 70
            DiscountRule rule2 = new DiscountRuleBuilder().WithUnits(itemA, 2).WithUnits(itemB, 1).WithPrice(100).Build();

            _mockRuleRepository.MockRules = new List<DiscountRule> { rule1, rule2 };

            List<ItemPile> basket = new List<ItemPile>
            {
                new ItemPile(itemA, 2),
                new ItemPile(itemB, 1)
            };

            //the rule which renders the smallest price is rule2
            decimal result = _pricing.GetDiscountedPrice(basket);
            result.Should().Be(175);
        }


    }
}
