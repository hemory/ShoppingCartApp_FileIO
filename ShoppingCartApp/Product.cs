namespace ShoppingCartApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }



        public void DecreaseInventory(int quantity)
        {
            Inventory -= quantity;
        }


    }
}
