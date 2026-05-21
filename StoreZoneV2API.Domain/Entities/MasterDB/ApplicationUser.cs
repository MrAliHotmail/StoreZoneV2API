using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Entities.MasterDB
{
    // هذا يمثل أي شخص يستطيع تسجيل الدخول:
   // Admin / Tenant Owner / Store Employee / Customer
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int? TenatId { get; private set; }
        public Tenant? Tenant { get; set; }

        public Customer? MyProperty { get; private set; }
        public StoreEmployee? StoreEmployee { get; private set; }

        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;

        public ApplicationUser() { } // For EF Core

        public ApplicationUser(string firstname , string lastname , string email , int? tenantId = null)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            UserName = email;
            TenatId = tenantId;
        }
    }
}

