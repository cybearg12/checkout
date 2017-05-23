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
        private readonly IUnitRepository _mockRepository = new MockUnitRepository();
        private IPricing _pricing;

        public PricingTest()
        {
            _pricing = new PricingController();
        }

        [Fact]
        public void GetDiscount_Price_For_One_Item_Is_Equal_To_Item_Price()
        {
            List<StockKeepingUnit> basket = new List<StockKeepingUnit>();
            basket.Add(new StockKeepingUnit("A", 50));
            
            decimal result =_pricing.GetDiscountedPrice(basket);
            result.Should().Be(50);            
        }
    }
}
