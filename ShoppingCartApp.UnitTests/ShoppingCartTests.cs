using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ShoppingCartApp.UnitTests
{
    [TestFixture]
    class ShoppingCartTests
    {
        [TestCase(4,4)]
        [TestCase(5,5)]
        [TestCase(10,10)]
        public void AddToCartBasedOnQuantity_AlwaysReturns_ExpectedResult(int quantity, int expected)
        {
            //Arrange
            Product product = new Product();
            ShoppingCart sut = new ShoppingCart();
            sut.Products = new List<Product>();

            //Act
            List<Product> actualCart = sut.AddToCartBasedOnQuantity(quantity,product);

            //Assert
            Assert.AreEqual(expected,actualCart.Count);
        }

        [Test]
        public void CalculateTotalCartPriceBeforeTax_AlwaysReturns_CorrectTotal()
        {
            //Arrange
            ShoppingCart sut = new ShoppingCart();
            sut.Products = new List<Product>();
            sut.Products.Add(new Product{Price = 2.00});
            sut.Products.Add(new Product{Price = 1.00});
            sut.Products.Add(new Product{Price = 5.00});

            //Act
            double actualTotal = sut.CalculateTotalCartPriceBeforeTax();

            //Assert
            Assert.AreEqual(8.00,actualTotal);
        }

        [TestCase("",10,10.60)]
        [TestCase("SAVEMONEY",10,9.54)]
        public void CalculateTotalCartPriceAfterTax_AlwaysReturns_CorrectTotal(string discountCode, double totalPriceBeforeTaxes, double expected)
        {
            //Arrange
            ShoppingCart sut = new ShoppingCart();
            
            //Act
            double actualTotal = sut.CalculateTotalCartPriceAfterTax(discountCode, totalPriceBeforeTaxes);

            //Assert
            Assert.AreEqual(expected,actualTotal);
        }
    }
}
