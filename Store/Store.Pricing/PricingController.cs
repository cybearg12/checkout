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

        public decimal GetDiscountedPrice(List<ItemPile> basket)
        {
            if (basket == null || basket.Count == 0)
                throw new ArgumentException("Items collection is empty");
            List<DiscountRule> appliedRules = new List<DiscountRule>();

            List<DiscountRule> allRules = _ruleRepository.GetRules().ToList(); //TODO: get only the rules which contain only items in the basket
            
            //apply the discount rules to the basket; TODO: this modified the state of the basket!
            foreach(DiscountRule currentRule in allRules)
            {
                if(IsMatchingRule(basket, currentRule))
                {
                    basket = ApplyDiscountRule(basket, currentRule);
                    appliedRules.Add(currentRule);
                }    
            }            

            return basket.Sum(p => p.Price) + appliedRules.Sum(r => r.Price);            
        }

        private List<ItemPile> ApplyDiscountRule(List<ItemPile> basket, DiscountRule currentRule)
        {
            List<ItemPile> discountedBasket = new List<ItemPile>();

            foreach (var basketPile in basket)
            {
                ItemPile rulePile = currentRule.Piles.FirstOrDefault(p => p.Unit == basketPile.Unit);
                if (rulePile != null)
                {
                    ItemPile discountedPile = new ItemPile(basketPile.Unit, basketPile.Quantity - rulePile.Quantity);
                    discountedBasket.Add(discountedPile);
                }
                else
                {
                    discountedBasket.Add(basketPile.Copy());
                }        
            }

            return discountedBasket;
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
