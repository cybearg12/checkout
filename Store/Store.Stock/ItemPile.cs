namespace Store.Stock
{
    public class ItemPile
    {
        public ItemPile(StockKeepingUnit unit, int quantity)
        {
            Unit = unit;
            Quantity = quantity;
        }

        public StockKeepingUnit Unit { get; private set; }
        public int Quantity { get; set; }

        public decimal Price
        {
            get { return Unit.Price * Quantity; }
        }
        
    }
}
