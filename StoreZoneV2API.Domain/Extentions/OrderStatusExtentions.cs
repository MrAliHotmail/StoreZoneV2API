using StoreZoneV2API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreZoneV2API.Domain.Extentions  
{
    public static class OrderStatusExtentions
    {
        //  this method for cheak if order have item or product and i want change this price or qunatity for this product
        public static bool IsActiveOrders(this OrderStatusEnum status)
        {
            return status == OrderStatusEnum.Pending ||
                   status == OrderStatusEnum.Processing ||
                   status == OrderStatusEnum.OnHold;
        }
        public static bool IsCompleted(this OrderStatusEnum status)
        {
            return status == OrderStatusEnum.Completed;
        }
        public static bool IsShipped(this OrderStatusEnum status)
        {
            return status == OrderStatusEnum.Shipped;
        }
        public static bool IsCanceled(this OrderStatusEnum status)
        {
            return status == OrderStatusEnum.Canceled;
        }
        
    }
}
