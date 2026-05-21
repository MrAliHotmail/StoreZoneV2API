using StoreZoneV2API.Domain.Enums;
using StoreZoneV2API.Domain.Entities.MasterDB;
using StoreZoneV2API.Domain.Common;

namespace StoreZoneV2API.Domain.Entities.TenantDB
{
    public class Order : BaseEntity
    {
        public int TenantId { get; private set; } // Foreign key to Tenant
        public Tenant Tenant { get; private set; } = null!; // Navigation property
        public string ApplicationUserId { get; private set; } = string.Empty; // Foreign key to TenantUser
        public ApplicationUser? ApplicationUser { get; private set; } = null!; // Navigation property
        public Address ShippingAddress { get; private set; } = null!; // عنوان الشحن الخاص بالطلب
        private readonly List<OrderItem> _items = new(); // المخزن الحقيقي للبيانات لكنها برايفت عشان ما حدش يغيرها من بره الكلاس
        public IReadOnlyCollection<OrderItem> OrderItems => _items.AsReadOnly(); //  ,لماذا؟ لكي تسمح للواجهة البرمجية (API) أو الطبقات الأخرى برؤية محتويات الطلب وعرضها، ولكن بدون القدرة على تعديلها.الغرض للقراءه فقط 
        public decimal TotalAmount => _items.Sum(x => x.TotalPrice);
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public OrderStatusEnum Status { get; private set; } = OrderStatusEnum.Pending; // اي طلب جديد يبدا بحالة pending ومن ثم يتم تغيرها حسب سير العمل

        private Order()
        {
            // For EF Core
        }


        public Order(string applicationUserId)
        {
            if (string.IsNullOrWhiteSpace(applicationUserId))
                throw new ArgumentNullException(nameof(applicationUserId), "Application user id is required.");

            ApplicationUserId = applicationUserId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatusEnum.Pending;
        }

        public bool HasActiveStatus()
        {
            return Status == OrderStatusEnum.Pending
                || Status == OrderStatusEnum.Processing;
                 
        }
        // 1-تأكد أن الطلب في حالة تسمح بإضافة عناصر جديدة (مثل "قيد الانتظار" أو "قيد المعالجة
        // 2- تحقق من أن المنتج الذي يتم إضافته موجود في قاعدة البيانات وأنه متاح للبيع.
        // 3- إذا كان المنتج موجودًا بالفعل في الطلب، قم بتحديث الكمية والسعر بدلاً من إضافة عنصر جديد.
        // 4- قم بحساب المبلغ الإجمالي للطلب بناءً على العناصر المضافة.
        // 5- تأكد من أن الكمية والسعر المقدمين صالحين (على سبيل المثال، الكمية يجب أن تكون أكبر من صفر والسعر يجب أن يكون غير سالب).
        // 6- قم بتحديث حالة الطلب إذا لزم الأمر (على سبيل المثال، إذا تم إضافة عنصر جديد، قد ترغب في تغيير الحالة إلى "قيد المعالجة").
        // 7- تأكد من أن هناك حدًا أقصى لعدد العناصر التي يمكن إضافتها إلى الطلب إذا كان ذلك مناسبًا لعملك.
        public void AddOrderItem(int productId, int quantity, decimal unitPrice)
        {
            if (!HasActiveStatus())
                throw new InvalidOperationException("Cannot add items to a completed or canceled order.");
            var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.ChangeQuantity(existingItem.Quantity + quantity);
                existingItem.ChangeUnitPrice(unitPrice); // تحديث السعر في حال تغير
                return; 
            }

            var item = new OrderItem(productId, quantity, unitPrice);
            _items.Add(item);
        }

        // 1-تأكد أن الطلب في حالة تسمح بتغيير العناصر (مثل "قيد الانتظار" أو "قيد المعالجة")
        public void ChangeItemQunatity(int productId, int newQuantity)  
        {
            if (!HasActiveStatus())
                throw new InvalidOperationException("Cannot change items in a completed or canceled order.");

            var exitingItem = _items.FirstOrDefault(x => x.ProductId == productId);
            if (exitingItem == null)
                throw new InvalidOperationException("Item not found in the order.");
            exitingItem.ChangeQuantity(newQuantity);
        }

        public void ChangeItemUnitPrice(int productId, decimal newUnitPrice)
        {
            if (!HasActiveStatus())
                throw new InvalidOperationException("Cannot change items in a completed or canceled order.");
            var exitingItem = _items.FirstOrDefault(x => x.ProductId == productId);
            if (exitingItem == null)
                throw new InvalidOperationException("Item not found in the order.");
            exitingItem.ChangeUnitPrice(newUnitPrice);
        }

        //  تأكد من أن الطلب ليس في حالة "مكتمل" أو "تم شحنه"، حيث لا يمكن تغيير عنوان الشحن لهذه الطلبات.
        public void ChangeShippingAddress(Address newAddress)
        {
            if (Status == OrderStatusEnum.Completed || Status == OrderStatusEnum.Canceled || Status == OrderStatusEnum.Shipped)
                throw new InvalidOperationException("Cannot change shipping address of a completed, canceled, or shipped order.");
            ShippingAddress = newAddress;
        }
        // 1-تأكد أن الطلب في حالة تسمح بتغيير العناصر (مثل "قيد الانتظار" أو "قيد المعالجة")
        public void ChangeStatus(OrderStatusEnum newStatus)
        {
            if (Status == OrderStatusEnum.Completed || Status == OrderStatusEnum.Shipped)
                throw new InvalidOperationException("Cannot change status of a completed, canceled, or shipped order.");
            Status = newStatus;
        }
        // 2- تحقق من أن الطلب ليس في حالة "مكتمل" أو "تم شحنه"، حيث لا يمكن إلغاء هذه الطلبات.
        public void CancelOrder()
        {
            if (Status == OrderStatusEnum.Completed || Status == OrderStatusEnum.Shipped)
                throw new InvalidOperationException("Cannot cancel a completed or shipped order.");
            Status = OrderStatusEnum.Canceled;
        }

    }
}