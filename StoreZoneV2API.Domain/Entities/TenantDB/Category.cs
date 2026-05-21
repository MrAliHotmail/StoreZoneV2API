using StoreZoneV2API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Category : BaseEntity
    {
       // public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }
}
