using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShoppingCartApp.UnitTests
{
    [TestFixture]
   public class FileHandlerTests
    {
        [Test]
        public void GetProducts_Always_ReturnsAListOfProductsFromTextFile()
        {
            //Arrange
            string path = @"C:\Users\hphifer\source\repos\ShoppingCartApp_UnitTests\ShoppingCartApp_UnitTests\ShoppingCartApp.UnitTests\testReadData.txt";

            //Act
           List<Product> actual = FileHandler.GetProducts(path);

            //Assert
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual("black coffee", actual[0].Name);
            Assert.AreEqual("latte", actual[1].Name);
            Assert.AreEqual("tea", actual[2].Name);
        }

        [Test]
        public void WriteDataToTextFile_Always_UpdatesTheTextFile()
        {
            //Arrange
            string path = @"C:\Users\hphifer\source\repos\ShoppingCartApp_UnitTests\ShoppingCartApp_UnitTests\ShoppingCartApp.UnitTests\testWriteData.txt";

            ProductRepo productRepo = new ProductRepo(path);

            List<Product> products = new List<Product>()
            {
                new Product(){Id= 0, Name = "coffee", Description = "decaf", Category = "beverage", Price = 1.00, Inventory = 2}
            };


            //Act
            FileHandler.WriteDataToTextFile(path, products);

            //Assert
            Assert.AreEqual(1, FileHandler.GetProducts(path).Count);
            Assert.AreEqual("coffee", FileHandler.GetProducts(path)[0].Name);
           
        }

    }

}
