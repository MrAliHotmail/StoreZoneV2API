using StoreZoneV2API.Domain.Common;
using StoreZoneV2API.Domain.Entities.MasterDB;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Customer : BaseEntity
    {
        public int TenantId { get; private set; }
        public Tenant Tenant { get; private set; } = null!;

        public string ApplicationUserId { get; private set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; private set; } = null!;

        private readonly List<CustomerAddress> _customerAddresses = new(); // 
        public IReadOnlyCollection<CustomerAddress> CustomerAddresses => _customerAddresses.AsReadOnly(); // نسخة للقراءة فقط من العناوين الخاصة بالعميل

        public List<Order> Orders { get; private set; } = new(); 

        private Customer() { } // For EF

        public Customer(int tenantId, string applicationUserId , string _customerAddress)
        {
            TenantId = tenantId;
            ApplicationUserId = applicationUserId;
            _customerAddresses.Add(new CustomerAddress { TenantId = tenantId, CustomerId = Id});

        }
    }
}