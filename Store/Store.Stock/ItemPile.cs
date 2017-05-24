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
        

        public decimal Price
        {
            get { return Unit.Price * Quantity; }
        }        

        public void AddToPile(int itemCount)
        {
            Quantity += itemCount;
        }

        public void RemoveFromPile(int itemCount)
        {
            if(itemCount > Quantity)
                throw new InvalidOperationException();

            Quantity -= itemCount;
        }

        public ItemPile Copy()
        {
            return new ItemPile(Unit, Quantity);
        }
    }
}
