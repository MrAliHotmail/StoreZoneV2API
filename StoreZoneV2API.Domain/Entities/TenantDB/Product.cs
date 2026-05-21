using StoreZoneV2API.Domain.Common;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public string imagesUrl { get; set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public bool InStock { get; private set; } = false;
        public int StockQuantity { get; private set; }
        public int CategoryId { get; private set; }
        public Category? Category { get; private set; }

        private Product()
        {

        }
        public Product(string name, decimal price, string imagesUrl)
        {

            setName(name);
         //   setPrice(price);
            this.imagesUrl = imagesUrl;
        }

        public void setName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("product name cannot be empty.");
            }
            Name = name;
        }

        public void setPrice(decimal price, bool hasActiveOrders)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }
            if (hasActiveOrders)
            {
                throw new Exception("Not allow change price becouse this product in Order");
            }
            Price = price;
        }
        public void UpdateStockQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be under zero.");
            }
            StockQuantity = quantity;
            InStock = quantity > 0;

        }
    }
}
