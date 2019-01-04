using System.Collections.Generic;

namespace ClothingStore.Data.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public List<VendorClothing> VendorClothings { get; set; }
    }
}
