using ClothingStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClothingStore.Data
{
    public class DataContext
    {
        public DataContext()
        {
            // Generate data
            Vendors = new List<Vendor>
            {
                // Vendor #1
                new Vendor
                {
                    Id = 1,
                    Name = "Levi's",
                    Balance = 50,
                    VendorClothings = new List<VendorClothing>
                    {
                        new VendorClothing
                        {
                            Quantity = 2,
                            Clothing = new TShirt
                            {
                                Id = 1,
                                Color = Color.Blue,
                                Size = Size.Medium
                            }
                        },
                        new VendorClothing
                        {
                            Quantity = 1,
                            Clothing = new TShirt
                            {
                                Id = 2,
                                Color = Color.Red,
                                Size = Size.Large
                            }
                        },
                        new VendorClothing
                        {
                            Quantity = 2,
                            Clothing = new DressShirt
                            {
                                Id = 3,
                                Color = Color.Red,
                                Size = Size.Medium
                            }
                        }
                    }
                },

                // Vendor #2
                new Vendor
                {
                    Id = 2,
                    Name = "NNM",
                    Balance = 30,
                    VendorClothings = new List<VendorClothing>
                    {
                        new VendorClothing
                        {
                            Quantity = 3,
                            Clothing = new TShirt
                            {
                                Id = 4,
                                Color = Color.Blue,
                                Size = Size.Large
                            }
                        },
                        new VendorClothing
                        {
                            Quantity = 4,
                            Clothing = new DressShirt
                            {
                                Id = 5,
                                Color = Color.Red,
                                Size = Size.Large
                            }
                        },
                        new VendorClothing
                        {
                            Quantity = 2,
                            Clothing = new DressShirt
                            {
                                Id = 6,
                                Color = Color.Blue,
                                Size = Size.Large
                            }
                        }
                    }
                }
            };
        }

        public Vendor GetVendor(int id)
        {
            // Get the vendor by ID
            Vendor vendor = Vendors.SingleOrDefault(x => x.Id == id);
            return vendor;
        }

        public List<Vendor> Vendors { get; set; }
    }
}
