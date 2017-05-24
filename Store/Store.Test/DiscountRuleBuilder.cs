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
        private List<ItemPile> _itemPiles = new List<ItemPile>();
        private decimal _discountPrice;

        public DiscountRuleBuilder WithUnits(StockKeepingUnit unit, int quantity)
        {
            ItemPile existinPile = _itemPiles.FirstOrDefault(i => i.Unit == unit);
            if (existinPile != null)
                existinPile.AddToPile(quantity);
            else 
                _itemPiles.Add(new ItemPile(unit, quantity));

            return this;
        }

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
                Piles = _itemPiles,
                Price = _discountPrice
            };

            return result;
        }
    }
}
