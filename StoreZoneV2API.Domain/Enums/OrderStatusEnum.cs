using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Processing = 2,
        Completed = 3, 
        Shipped = 4,
        Canceled = 5,
        OnHold = 6,
        IsActive = 7,
        Paid = 8,
        Refunded = 9

    }
}
