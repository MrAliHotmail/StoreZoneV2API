using StoreZoneV2API.Domain.Enums;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public Order? Order { get; set; } // Navigation property to Order
        public int ProductId { get; private set; } // Foreign key to Product
        public Product? Product { get; set; } // Navigation property to Product
        public int Quantity { get; private set; } // Quantity of the product ordered
        public decimal UnitPrice { get; private set; } // Price at the time of order
        public decimal TotalPrice => Quantity * UnitPrice; // Total price for this order item
        //==================================

        private OrderItem()
        {
            // For EF Core
        }

        public OrderItem(int productId, int qunatity , decimal unitprice)
        {
            if(qunatity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
            if (unitprice <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }
            ProductId = productId;
            Quantity = qunatity;
            UnitPrice = unitprice;
        }

        
        internal void ChangeUnitPrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.", nameof(newPrice));
            }
            UnitPrice = newPrice;
        }   

        internal void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }
            Quantity = quantity;
        }

    }
}