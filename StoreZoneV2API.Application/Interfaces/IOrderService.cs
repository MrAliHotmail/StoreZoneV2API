using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        // This method checks if there are any active orders for a given product ID.
        // for example, it could return true if there are pending orders that include the specified product,
        // and false if there are no active orders for that product. This is useful for determining whether certain actions
        // (like changing the price of a product) can be performed without affecting existing orders.

        //Task<bool> HasActiveOrderAsync(int productId);

    }
}
