using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShoppingCartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFilePath = @"C:\Users\hphifer\source\repos\ShoppingCart_FileIO\ShoppingCartApp\data.txt";

            ProductRepo productsFromRepo = new ProductRepo(textFilePath);
            ShoppingCart cart = new ShoppingCart();
            var highestId = productsFromRepo.Products.Max(p => p.Id);
            bool continueToRegularApp = true;


            //Admins List
            List<Administrator> admins = new List<Administrator>()
            {
                new Administrator("hem", 1234)
            };

            Console.Write("Enter 'admin' to access admin menu or press any key to continue: ");



            string accessAdminMenu = Console.ReadLine().ToLower().Trim();

            if (accessAdminMenu == "admin")
            {
                bool isAdmin = false;

                for (int i = 0; i <= 4; i++)
                {
                    Console.Write("User Name: ");
                    string userName = Console.ReadLine();

                    while (string.IsNullOrEmpty(userName))
                    {
                        Console.Write("Try again: ");
                        userName = Console.ReadLine().ToLower().Trim();
                    }


                    Console.Write("Pin: ");

                    bool isInt = int.TryParse(Console.ReadLine(), out int pin);
                    while (isInt == false)
                    {
                        Console.Write("Pin: ");
                        isInt = int.TryParse(Console.ReadLine(), out pin);
                    }

                    Thread.Sleep(2000);
                    Console.Clear();

                    foreach (var admin in admins)
                    {
                        if (userName == admin.UserName && pin == admin.Pin)
                        {
                            isAdmin = true;
                            bool keepGoing = true;

                            while (keepGoing)
                            {
                                Console.WriteLine("[1]Add Product [2]Update Product Inventory [3]Delete Product");
                                string adminChoice = Console.ReadLine().Trim();

                                while (string.IsNullOrEmpty(adminChoice))
                                {
                                    Console.Write("Try again: ");
                                    adminChoice = Console.ReadLine().ToLower().Trim();
                                }

                                Thread.Sleep(1000);
                                Console.Clear();

                                switch (adminChoice)
                                {
                                    case "1":

                                        Product product = new Product();

                                        keepGoing = false;
                                        product.Id = highestId + 1;
                                        Console.Write("Name: ");
                                        product.Name = Console.ReadLine().ToLower().Trim();

                                        while (string.IsNullOrEmpty(product.Name))
                                        {
                                            Console.Write("Try again: ");
                                            product.Name = Console.ReadLine().ToLower().Trim();
                                        }


                                        Console.Write("Description: ");
                                        product.Description = Console.ReadLine().ToLower().Trim();

                                        while (string.IsNullOrEmpty(product.Description))
                                        {
                                            Console.Write("Try again: ");
                                            product.Description = Console.ReadLine().ToLower().Trim();
                                        }


                                        Console.Write("Category: ");
                                        product.Category = Console.ReadLine().ToLower().Trim();

                                        while (string.IsNullOrEmpty(product.Category))
                                        {
                                            Console.Write("Try again: ");
                                            product.Category = Console.ReadLine().ToLower().Trim();
                                        }



                                        Console.Write("Price: ");
                                        isInt = int.TryParse(Console.ReadLine(), out int price);
                                        while (isInt == false)
                                        {
                                            Console.Write("Price: ");
                                            isInt = int.TryParse(Console.ReadLine(), out price);
                                        }

                                        product.Price = price;





                                        Console.Write("Inventory: ");

                                        isInt = int.TryParse(Console.ReadLine(), out int inventory);
                                        while (isInt == false)
                                        {
                                            Console.Write("Inventory: ");
                                            isInt = int.TryParse(Console.ReadLine(), out inventory);
                                        }

                                        product.Inventory = inventory;



                                        productsFromRepo.Products.Add(product);

                                        Console.WriteLine();

                                        Console.WriteLine("Here is your updated list.");

                                        DisplayProductsForAdmin(productsFromRepo);

                                        FileHandler.WriteDataToTextFile(textFilePath, productsFromRepo.Products);

                                        break;

                                    case "2":
                                        keepGoing = false;

                                        DisplayProductsForAdmin(productsFromRepo);


                                        Console.Write("Please choose one to update: ");

                                        isInt = int.TryParse(Console.ReadLine(), out int idToUpdate);
                                        while (isInt == false)
                                        {
                                            Console.Write("Please choose one to update: ");
                                            isInt = int.TryParse(Console.ReadLine(), out idToUpdate);
                                        }



                                        Thread.Sleep(2000);
                                        Console.Clear();

                                        var productToUpdate = productsFromRepo.Products.Find(p => p.Id == idToUpdate);

                                        productsFromRepo.Products.Remove(productToUpdate);

                                        Console.WriteLine($"The current inventory is {productToUpdate.Inventory}.");
                                        Console.Write("How many would you like to add to the inventory: ");

                                        isInt = int.TryParse(Console.ReadLine(), out int newInventory);
                                        while (isInt == false)
                                        {
                                            Console.Write("How many would you like to add to the inventory: ");
                                            isInt = int.TryParse(Console.ReadLine(), out newInventory);
                                        }

                                        productToUpdate.Inventory += newInventory;




                                        productsFromRepo.Products.Add(productToUpdate);

                                        Console.WriteLine("Here is your updated list.");

                                        DisplayProductsForAdmin(productsFromRepo);

                                        FileHandler.WriteDataToTextFile(textFilePath, productsFromRepo.Products);


                                        break;

                                    case "3":
                                        keepGoing = false;
                                        DisplayProducts(productsFromRepo);
                                        Console.WriteLine();
                                        Console.Write("Please choose one to delete: ");

                                        isInt = int.TryParse(Console.ReadLine(), out int idToDelete);
                                        while (isInt == false)
                                        {
                                            Console.Write("Please choose one to delete: ");
                                            isInt = int.TryParse(Console.ReadLine(), out idToDelete);
                                        }


                                        Thread.Sleep(2000);
                                        Console.Clear();

                                        var productToDelete = productsFromRepo.Products.Find(p => p.Id == idToDelete);

                                        productsFromRepo.Products.Remove(productToDelete);

                                        Console.WriteLine("Product has been removed. This is your current product list.");
                                        Console.WriteLine();

                                        DisplayProducts(productsFromRepo);

                                        FileHandler.WriteDataToTextFile(textFilePath, productsFromRepo.Products);


                                        Thread.Sleep(2000);
                                        Console.Clear();

                                        break;

                                    default:
                                        Console.WriteLine("Please choose a valid option");
                                        break;
                                }

                                Thread.Sleep(2000);
                                Console.Clear();

                                Console.Write("Return to Admin Menu? Enter [y] to return or any key to exit application: ");
                                string choiceToReturnToAdminMenu = Console.ReadLine().ToLower().Trim();


                                if (choiceToReturnToAdminMenu == "y")
                                {
                                    keepGoing = false;
                                }
                            }
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

                Console.Write("Continue to regular app? Enter [n] to exit or any key to continue to regular app: ");
                string response = Console.ReadLine().ToLower().Trim();


                if (response == "n")
                {
                    continueToRegularApp = false;
                }

                Thread.Sleep(2000);
                Console.Clear();


            }


            if (continueToRegularApp)
            {
                string readyToCheckout;
                do
                {
                    Console.WriteLine($"Welcome to the Shopping Cart App! {Environment.NewLine}");

                    Console.WriteLine("Lets get you to our list...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("Welcome to the Beverage Bodega!");


                    bool willAddAnother = true;
                    bool isValidChoice = false;

                    while (willAddAnother)
                    {
                        Console.WriteLine(Environment.NewLine);
                        Console.Write("Choose an option: ");

                        DisplayProducts(productsFromRepo);
                        
                        bool isInt = int.TryParse(Console.ReadLine(), out int itemChoice);
                        while (isInt == false)
                        {
                            Console.Write("Choose an option: ");
                            isInt = int.TryParse(Console.ReadLine(), out itemChoice);
                        }


                        foreach (var product in productsFromRepo.Products)
                        {
                            if (itemChoice == product.Id)
                            {
                                isValidChoice = true;
                                bool tooManySelected = true;
                                while (tooManySelected)
                                {
                                    Console.WriteLine("How many would you like?");

                                    isInt = int.TryParse(Console.ReadLine(), out int quantity);
                                    while (isInt == false)
                                    {
                                        Console.WriteLine("How many would you like?");
                                        isInt = int.TryParse(Console.ReadLine(), out quantity);
                                    }


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
                        }

                        if (isValidChoice == false)
                        {
                            continue;
                        }


                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.Write("Would you like to add another item to your cart? (y)es or (n)o: ");
                        string addAnotherItem = Console.ReadLine().ToLower().Trim();

                        while (string.IsNullOrEmpty(addAnotherItem))
                        {
                            Console.Write("Would you like to add another item to your cart? (y)es or (n)o: ");
                            addAnotherItem = Console.ReadLine().ToLower().Trim();
                        }


                        if (addAnotherItem != "y")
                        {
                            willAddAnother = false;
                        }

                        Console.Clear();
                    }


                    Console.Write("Are you ready to check out? (y)es or (n)o: ");
                    readyToCheckout = Console.ReadLine().ToLower().Trim();

                    while (string.IsNullOrEmpty(readyToCheckout))
                    {
                        Console.Write("Are you ready to check out? (y)es or (n)o: ");
                        readyToCheckout = Console.ReadLine().ToLower().Trim();
                    }

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
            }


            Console.WriteLine($"Thank you for choosing us! {Environment.NewLine}");

            Console.WriteLine("Press ENTER to exit");

            //todo overwrite file here

            FileHandler.WriteDataToTextFile(textFilePath, productsFromRepo.Products);

            Console.ReadLine();
        }


        private static void DisplayProducts(ProductRepo productRepo)
        {
            Console.WriteLine("**********************************************************");

            //display the repo
            foreach (var item in productRepo.Products)
            {
                Console.WriteLine($"({item.Id}) {item.Name.ToUpper()} | {item.Description.ToUpper()} | {item.Category.ToUpper()} | {item.Price}");
            }
            Console.WriteLine("**********************************************************");
            Console.WriteLine();

        }

        private static void DisplayProductsForAdmin(ProductRepo productRepo)
        {
            Console.WriteLine("**********************************************************");

            //display the repo
            foreach (var item in productRepo.Products)
            {
                Console.WriteLine($"({item.Id}) {item.Name.ToUpper()} | {item.Description.ToUpper()} | {item.Category.ToUpper()} | {item.Price} | {item.Inventory}");
            }
            Console.WriteLine("**********************************************************");
            Console.WriteLine();

        }

    }

}
