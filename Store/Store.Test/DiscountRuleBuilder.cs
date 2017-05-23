using System;
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
        private List<ItemPile> _itemPiles;
        private decimal _discountPrice;

        public DiscountRuleBuilder ForItems(List<ItemPile> piles)
        {
            _itemPiles = piles;          

            return this;
        }

        public DiscountRuleBuilder WithPrice(decimal price)
        {
            _discountPrice = price;
            return this;
        }              

        public DiscountRule Build()
        {
            DiscountRule result = new DiscountRule
            {
                ItemGroups = _itemPiles,
                Price = _discountPrice
            };

            return result;
        }
    }
}
