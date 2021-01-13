using System;
using System.Collections.Generic;
using System.Threading;

namespace ShoppingCartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFilePath = @"C:\Users\hphifer\source\repos\ShoppingCartApp_UnitTests\ShoppingCartApp_UnitTests\ShoppingCartApp\data.txt";

            ProductRepo productsFromRepo = new ProductRepo(textFilePath);
            ShoppingCart cart = new ShoppingCart();


            List<Administrator> admins = new List<Administrator>()
            {
                new Administrator("hem", 1234)
            };

            Console.WriteLine("Enter 'admin' to access admin menu or press any key to continue");
            string accessAdminMenu = Console.ReadLine().ToLower().Trim();


            if (accessAdminMenu == "admin")
            {
                bool isAdmin = false;

                for (int i = 0; i <= 4; i++)
                {
                    Console.Write("User Name: ");
                    string userName = Console.ReadLine();


                    Console.Write("Pin: ");
                    int pin = int.Parse(Console.ReadLine());


                    foreach (var admin in admins)
                    {
                        if (userName == admin.UserName && pin == admin.Pin)
                        {
                            isAdmin = true;
                            //todo admin menu
                            Console.WriteLine("Your in!");
                        }
                        else
                        {
                            if (i <= 3)
                            {
                                Console.WriteLine("Invalid user name or pin. Try again."); 
                            }
                            break;
                        }
                    }


                    if (isAdmin)
                    {
                      break;
                    }
                }

                Console.WriteLine();
                if (isAdmin == false)
                {
                    Console.WriteLine("Sorry, we cannot verify you as an admin."); 
                }
                Thread.Sleep(2000);
                Console.Clear();

            }


            string readyToCheckout;
            do
            {
                Console.WriteLine($"Welcome to the Shopping Cart App! {Environment.NewLine}");

                Console.WriteLine("Lets get you to our list...");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Welcome to the Beverage Bodega!");


                bool willAddAnother = true;

                while (willAddAnother)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Choose an option");

                    Console.WriteLine("**********************************************************");
                    DisplayProducts(productsFromRepo);
                    Console.WriteLine();
                    Console.WriteLine("**********************************************************");

                    int itemChoice = int.Parse(Console.ReadLine());


                    foreach (var product in productsFromRepo.Products)
                    {
                        if (itemChoice == product.Id)
                        {
                            bool tooManySelected = true;
                            while (tooManySelected)
                            {
                                Console.WriteLine("How many would you like?");
                                int quantity = Convert.ToInt16(Console.ReadLine());

                                if (quantity > product.Inventory)
                                {
                                    Console.WriteLine($"There are only {Math.Abs(product.Inventory)} left in stock. Please choose fewer");
                                    continue;
                                }
                                product.DecreaseInventory(quantity);
                                cart.Products = cart.AddToCartBasedOnQuantity(quantity, product);
                                Console.WriteLine($"{quantity } {product.Name}'s have been added to the cart.");
                                tooManySelected = false;

                            }
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option.");
                            break;
                        }
                    }


                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Would you like to add another item? (y)es or (n)o");
                    string addAnotherItem = Console.ReadLine().ToLower().Trim();

                    if (addAnotherItem != "y")
                    {
                        willAddAnother = false;
                    }

                    Console.Clear();
                }


                Console.WriteLine("Are you ready to check out? (y)es or (n)o");
                readyToCheckout = Console.ReadLine().ToLower().Trim();
                Console.Clear();

            } while (readyToCheckout != "y");


            double totalCartPriceBeforeTax = cart.CalculateTotalCartPriceBeforeTax();


            Console.WriteLine($"The total cart price before tax is {totalCartPriceBeforeTax:C} {Environment.NewLine}");

            Console.WriteLine($"*****Enter SAVEMONEY to save 10 percent****** {Environment.NewLine}");

            Console.WriteLine("Please type in discount code or press Enter to continue");
            Console.Write("Discount Code: ");
            string discountCode = Console.ReadLine().ToUpper().Trim();

            Console.Clear();

            double totalCartPriceAfterTax =
                cart.CalculateTotalCartPriceAfterTax(discountCode, totalCartPriceBeforeTax);
            Console.WriteLine($"The new total cart price before tax is {totalCartPriceBeforeTax:C} {Environment.NewLine}");
            Console.WriteLine($"The new total cart price after tax is {totalCartPriceAfterTax:C} {Environment.NewLine}");
            Thread.Sleep(3000);
            Console.Clear();


            Console.WriteLine($"Thank you for choosing us! {Environment.NewLine}");

            Console.WriteLine("Press ENTER to exit");

            //todo overwrite file here

            FileHandler.WriteDataToTextFile(textFilePath, productsFromRepo.Products);

            Console.ReadLine();
        }


        private static void DisplayProducts(ProductRepo productRepo)
        {

            int displayNumber = 1;

            //display the repo
            foreach (var item in productRepo.Products)
            {
                Console.WriteLine($"({displayNumber}) {item.Name.ToUpper()} | {item.Description.ToUpper()} | {item.Category.ToUpper()} | {item.Price}");

                displayNumber++;
            }

        }

    }

}
