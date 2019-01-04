using System.Collections.Generic;

namespace ClothingStore.Data.Models
{
    public abstract class Clothing
    {
        public int Id { get; set; }

        public Size Size { get; set; }

        public Color Color { get; set; }

        public double Price { get; set; }

        public double RetailPrice { get; set; }

        public List<VendorClothing> VendorClothings { get; set; }
    }
}
