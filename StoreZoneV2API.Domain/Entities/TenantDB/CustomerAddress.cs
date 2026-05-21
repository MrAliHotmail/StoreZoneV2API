using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
    }
}
