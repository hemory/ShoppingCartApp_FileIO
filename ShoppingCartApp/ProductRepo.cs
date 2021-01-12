using System.Collections.Generic;

namespace ShoppingCartApp
{
    public class ProductRepo
    {

        public List<Product> Products { get; set; }

        public ProductRepo(string path)
        {
            Products = FileHandler.GetProducts(path);
        }

    }
}
