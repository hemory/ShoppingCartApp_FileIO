using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp
{
   public class FileHandler
   {
       public static List<Product> GetProducts(string path)
        {
            string[] linesFromFile = File.ReadAllLines(path);

            List<Product> products = new List<Product>();


            foreach (var line in linesFromFile)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    Product product = new Product();

                    string[] productArray = line.Split('|');
                    product.Id = int.Parse(productArray[0]);
                    product.Name = productArray[1];
                    product.Description = productArray[2];
                    product.Category = productArray[3];
                    product.Price = double.Parse(productArray[4]);
                    product.Inventory = int.Parse(productArray[5]);

                    products.Add(product);
                }
            }

            return products;
        }

        public static void WriteDataToTextFile(string path, List<Product> products)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var product in products)
                {
                    writer.WriteLine($"{product.Id}|{product.Name}|{product.Description}|{product.Category}|{product.Price}|{product.Inventory}");
                }
            }
        }
    }
}
