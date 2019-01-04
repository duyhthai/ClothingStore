using ClothingStore.Core;
using ClothingStore.Data;
using ClothingStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore
{
    class Program
    {
        private static DataContext _dataContext = new DataContext();
        private static Vendor vendor = null;

        static void Main(string[] args)
        {
            bool shouldContinue = true;
            Console.WriteLine("Welcome to The Clothing Store!");

            while (vendor == null)
            {
                // Get which vendor is using the app
                Console.Write("Please login with your vendor ID: ");
                vendor = _dataContext.GetVendor(ConsoleHelper.ReadInteger() ?? 0);

                if (vendor != null)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Hello {vendor.Name}!");

                    while (shouldContinue)
                    {
                        // Show vendor's latest info
                        ShowVendorInfo(vendor);

                        // Ask the vendor what to do and execute
                        Console.WriteLine("What do you want to do next?");
                        Console.WriteLine("1: Buy");
                        Console.WriteLine("2: Sell");
                        Console.WriteLine("3: Exit");
                        shouldContinue = ExecuteOrder(ConsoleHelper.ReadInteger());
                    }
                }
                else
                {
                    // Vendor not found
                    Console.WriteLine("Invalid ID!");
                }                
            }            
        }

        private static void ShowVendorInfo(Vendor vendor)
        {
            Console.WriteLine("---------------------------------------------");

            // Balance info
            Console.WriteLine($"Your current balance: ${vendor.Balance}");

            // Clothing info
            if (vendor.VendorClothings != null && vendor.VendorClothings.Any())
            {
                Console.WriteLine("Your current clothing stock:");
                foreach (VendorClothing vendorClothing in vendor.VendorClothings.Where(x => x.Clothing != null))
                {
                    Clothing clothing = vendorClothing.Clothing;
                    Console.WriteLine($"#{clothing.Id} - {clothing.GetType().Name}" +
                        $" {clothing.Color} {clothing.Size} - {vendorClothing.Quantity} items");
                }
            }
            else
            {
                Console.WriteLine("You have no clothing in stock.");
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
        }

        private static bool ExecuteOrder(int? actionId)
        {
            Console.WriteLine();

            try
            {
                switch (actionId)
                {
                    case 1:
                        // Buy
                        BuyClothing();
                        break;
                    case 2:
                        // Sell
                        SellClothing();
                        break;
                    case 3:
                        // Exit
                        return false;
                    default:
                        Console.WriteLine("Invalid action!");
                        break;
                }
            }
            catch (Exception ex)
            {
                // TODO: log exception
                Console.WriteLine("Execution failed. Please try again.");
            }

            // Continue the program
            Console.WriteLine();
            return true;
        }

        private static void BuyClothing()
        {
            Clothing clothing = null;

            // Select clothing type
            Console.WriteLine("Which clothing do you want to buy?");
            Console.WriteLine("1: T-Shirt: $6");
            Console.WriteLine("2: Dress Shirt: $8");
            clothing = SetClothingType();

            // Select clothing color
            SetClothingColor(clothing);

            // Select clothing size
            SetClothingSize(clothing);

            if (vendor.Balance >= clothing.Price)
            {
                // Save the clothing for current vendor
                VendorClothing existingItem = _dataContext.GetVariantFromVendorClothings(vendor.VendorClothings, clothing);
                if (existingItem != null)
                {
                    // Already have item with same type, color, size => increase quantity
                    existingItem.Quantity++;
                }
                else
                {
                    // Not existing => add new
                    clothing.Id = vendor.VendorClothings.Count + 1;
                    vendor.VendorClothings.Add(new VendorClothing
                    {
                        Clothing = clothing,
                        Quantity = 1
                    });
                }

                // Subtract vendor's balance with the item's price
                vendor.Balance -= clothing.Price;

                // Notify
                Console.WriteLine("Bought successfully!");
            }
            else
            {
                // Not enough money to buy
                Console.WriteLine("You don't have enough money to buy that item.");
            }
        }

        private static void SellClothing()
        {
            Clothing clothing = null;

            // Select clothing type
            Console.WriteLine("Which clothing do you want to sell?");
            Console.WriteLine("1: T-Shirt: $12");
            Console.WriteLine("2: Dress Shirt: $20");
            clothing = SetClothingType();

            // Select clothing color
            SetClothingColor(clothing);

            // Select clothing size
            SetClothingSize(clothing);

            // Check if the vendor has the item in stock
            VendorClothing existingItem = _dataContext.GetVariantFromVendorClothings(vendor.VendorClothings, clothing);
            if (existingItem != null)
            {
                if (existingItem.Quantity > 1)
                {
                    // More than 1 item => decrease quantity
                    existingItem.Quantity--;
                }
                else
                {
                    // Just 1 item => remove from stock
                    vendor.VendorClothings.Remove(existingItem);
                }

                // Increase the vendor's balance with the item's retail price
                vendor.Balance += clothing.RetailPrice;

                // Notify
                Console.WriteLine("Sold successfully!");
            }
            else
            {
                // Not existing => cannot sell
                Console.WriteLine("You don't have that item in stock to sell.");
            }
        }

        private static Clothing SetClothingType()
        {
            switch (ConsoleHelper.ReadInteger())
            {
                case 1:
                    return new TShirt();
                case 2:
                    return new DressShirt();
                default:
                    return null;
            }
        }

        private static void SetClothingColor(Clothing clothing)
        {
            Console.WriteLine("Please select a color:");
            Console.WriteLine("1: Red");
            Console.WriteLine("2: Blue");
            switch (ConsoleHelper.ReadInteger())
            {
                case 1:
                    clothing.Color = Color.Red;
                    break;
                case 2:
                    clothing.Color = Color.Blue;
                    break;
            }
        }

        private static void SetClothingSize(Clothing clothing)
        {
            Console.WriteLine("Please select your size:");
            Console.WriteLine("1: Medium");
            Console.WriteLine("2: Large");
            switch (ConsoleHelper.ReadInteger())
            {
                case 1:
                    clothing.Size = Size.Medium;
                    break;
                case 2:
                    clothing.Size = Size.Large;
                    break;
            }
        }
    }
}
