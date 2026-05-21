using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.ValueObjects
{
    // هذا الكلاس يمثل حدود الخطة الرقمية فقط من حيث عدد المنتجات والمستخدمين والعملاء اللي يقدروا يستخدموا المتجر
    public class PlaneLimits
    {
        public int? MaxProducts { get; set; }
        public int? MaxUsers { get; set; }
        public int? MaxCustomers { get; set; }

        private PlaneLimits() { } // for EF 

        public PlaneLimits(int? maxProducts, int? maxUsers, int? maxCustomers)
        {
              ValidateLimits(maxProducts, nameof(maxProducts));
              ValidateLimits(maxUsers, nameof(maxUsers));
              ValidateLimits(maxCustomers, nameof(maxCustomers));
    
              MaxProducts = maxProducts;
              MaxUsers = maxUsers;
              MaxCustomers = maxCustomers;
        }

        // Helper method to validate that limits are non-negative
        private static void ValidateLimits(int? value , string propertyName)
        {
            if(value.HasValue && value.Value < 0)
             
                throw new ArgumentException(" Limits value must be Greater than or equal to zero.", propertyName);
        }

    }
}
