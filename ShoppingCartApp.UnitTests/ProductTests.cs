using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShoppingCartApp.UnitTests
{
    [TestFixture]
    class ProductTests
    {
        [TestCase(2,8)]
        [TestCase(3,7)]
        [TestCase(1,9)]
        public void DecreaseInventory_AlwaysReturns_ExpectedResult(int quantity, int expected)
        {
            //Arrange
            Product sut = new Product();
            sut.Inventory = 10;

            //Act
           sut.DecreaseInventory(quantity);


            //Assert
            Assert.AreEqual(expected, sut.Inventory);
        }

       

        

       
    }
}
