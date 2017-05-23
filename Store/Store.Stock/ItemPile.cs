namespace Store.Stock
{
    public class ItemPile
    {
        public ItemPile(StockKeepingUnit unit, int quantity)
        {
            Item = unit;
            Quantity = quantity;
        }

        public StockKeepingUnit Item { get; private set; }
        public int Quantity { get; private set; }

        public decimal Price
        {
            get { return Item.Price * Quantity; }
        }
        
    }
}
