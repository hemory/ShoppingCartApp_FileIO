using System.Collections.Generic;

namespace ShoppingCartApp
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }


        public ShoppingCart()
        {
            Products = new List<Product>();
        }

        public List<Product> AddToCartBasedOnQuantity(int quantity, Product product)
        {
            for (int i = 0; i < quantity; i++)
            {
                Products.Add(product);
            }


            return Products;
        }

        public double CalculateTotalCartPriceBeforeTax()
        {
            double totalCartPrice = 0;
            //Shopping cart total
            foreach (var product in Products)
            {
                totalCartPrice += product.Price;
            }

            return totalCartPrice;
        }

        public double CalculateTotalCartPriceAfterTax(string discountCode, double totalCartPriceBeforeTax)
        {
            double discountPrice = ApplyDiscountCode(discountCode, totalCartPriceBeforeTax);


            double taxAmount = discountPrice * .06;
            double totalCartPriceWithTax = discountPrice + taxAmount;


            return totalCartPriceWithTax;
        }


        private static double ApplyDiscountCode(string discountCode, double totalCartPriceBeforeTax)
        {
            double discountPrice = 0;

            if (!string.IsNullOrEmpty(discountCode) && discountCode == "SAVEMONEY")
            {
                discountPrice = totalCartPriceBeforeTax - .10 * totalCartPriceBeforeTax;
            }
            else
            {
                discountPrice = totalCartPriceBeforeTax;
            }

            return discountPrice;
        }


    }
}
