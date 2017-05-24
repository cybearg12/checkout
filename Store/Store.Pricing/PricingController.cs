using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Stock;

namespace Store.Pricing
{
    public class PricingController : IPricing
    {
        private readonly IRuleRepository _ruleRepository;

        public PricingController(IRuleRepository repository)
        {
            _ruleRepository = repository;
        }

        public decimal GetDiscountedPrice(List<ItemPile> basketItems)
        {
            if (basketItems == null || basketItems.Count == 0)
                throw new ArgumentException("Items collection is empty");
            List<DiscountRule> appliedRules = new List<DiscountRule>();

            List<DiscountRule> allRules = _ruleRepository.GetRules().ToList(); //TODO: get only the rules which contain only items in the basket
            
            List<ItemPile> discountedBasket = new List<ItemPile>(basketItems.Select(p => p.Copy())); //create a copy of the basket
            foreach (DiscountRule currentRule in allRules)
            {
                if(IsMatchingRule(discountedBasket, currentRule))
                {
                    ApplyDiscountRule(discountedBasket, currentRule);
                    appliedRules.Add(currentRule);
                }    
            }            

            return discountedBasket.Sum(p => p.Price) + appliedRules.Sum(r => r.Price);  // TODO: this could change
        }

        private void ApplyDiscountRule(List<ItemPile> basket, DiscountRule currentRule)
        {
            foreach (var basketPile in basket)
            {
                ItemPile rulePile = currentRule.Piles.FirstOrDefault(p => p.Unit == basketPile.Unit);
                if (rulePile != null)
                {
                    basketPile.RemoveFromPile(rulePile.Quantity);
                }
            }
        }
        

        public bool IsMatchingRule(List<ItemPile> basket, DiscountRule rule)
        {
            foreach (ItemPile rulePile in rule.Piles)
            {
                //not all rules can be applied, so need to check the quantity in the basket 
                ItemPile basketPile = basket.FirstOrDefault(p => p.Unit == rulePile.Unit && p.Quantity >= rulePile.Quantity);
                if (basketPile == null)
                    return false;
            }

            return true;
        }
        

    }
}
