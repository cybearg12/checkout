using System;

namespace Store.Stock
{
    public class ItemPile
    {
        public ItemPile(StockKeepingUnit unit, int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException();
            Unit = unit;
            Quantity = quantity;           
            
        }      

        public StockKeepingUnit Unit { get; private set; }
        public int Quantity { get; private set; }
        public decimal DiscountedPrice { get; private set; }

        public decimal Price
        {
            get { return Unit.Price * Quantity; }
        }        

        //public ItemPile GetDiscountedPile(ItemPile discountRulePile)
        //{
        //    if (discountRulePile.Unit != Unit)
        //        return null;

        //    if (discountRulePile.Quantity < Quantity)
        //        return null;

        //    return new ItemPile(Unit, Quantity - discountRulePile.Quantity);            
        //}

        public void RemoveFromPile(ItemPile rulePile)
        {
            if (rulePile.Unit != Unit || rulePile.Quantity < Quantity)
                return;

            Quantity -= rulePile.Quantity;
        }

        public void AddToPile(int itemCount)
        {
            Quantity += itemCount;
        }

        public ItemPile Copy()
        {
            return new ItemPile(Unit, Quantity);
        }
    }
}
