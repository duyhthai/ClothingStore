namespace ClothingStore.Data.Models
{
    public class VendorClothing
    {
        public int VendorId { get; set; }

        public int ClothingId { get; set; }

        public int Quantity { get; set; }

        public Vendor Vendor { get; set; }

        public Clothing Clothing { get; set; }
    }
}
