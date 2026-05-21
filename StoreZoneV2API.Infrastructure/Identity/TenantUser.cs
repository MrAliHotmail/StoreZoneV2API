using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Infrastructure.Identity
{
    public class TenantUser : IdentityUser
    {
        
        public string Name { get; set; } = string.Empty;
        public int MobileNumber { get; set; }
        public int StoreId { get; set; } // Foreign key to Store
        public string StoreName { get; set; } = string.Empty; // Store name for easier access
        public int CommercialRegistrationNumber { get; set; }
        public List<Store> Stores { get; set; } = new List<Store>();
        public TenantUser( string name, int mobileNumber, int commercialRegistrationNumber)
        {
           
            Name = name;
            MobileNumber = mobileNumber;
            CommercialRegistrationNumber = commercialRegistrationNumber;
        }

    }
}
