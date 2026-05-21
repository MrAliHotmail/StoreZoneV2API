using StoreZoneV2API.Domain.Common;
using StoreZoneV2API.Domain.Entities.TenantDB;
using StoreZoneV2API.Domain.Enums;

namespace StoreZoneV2API.Domain.Entities.MasterDB
{
    //  هذا يمثل المستأجر (العميل) الذي يملك المتاجر ويستطيع إدارة موظفيه وعملائه
    // هذا يمثل المتجر أو الشركة المشتركة في منصة SaaS.
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // الاسم الرسمي للمستأجر (مثل اسم الشركة أو المتجر) ويستخدم في النظام الداخلي
        public string StoreName { get; private set; } = string.Empty; // اسم المتجر الذي يظهر للعملاء
        public string? Domain { get; set; } // النطاق الخاص بالمستأجر (مثل tenant1.storezone.com) ويستخدم للوصول إلى المتجر عبر الإنترنت
        public string? Description { get; set; } // وصف المتجر أو الشركة
        public string? LogoUrl { get; set; } // /uploads/logos/store1.png or cdn.storezone.com/logos/store1.png

        public string OwnerUserId { get; private set; } = string.Empty; // معرف المستخدم الذي يملك هذا المستأجر (المالك الرئيسي) ويستخدم لتحديد من هو المسؤول عن إدارة هذا المستأجر
        public ApplicationUser OwnerUser { get; private set; } = null!;

        public int SubscriptionPlanId { get; private set; }
        public SubscriptionPlan SubscriptionPlan { get; private set; } = null!;

        public TenantDatabase? TenantDatabase { get; private set; } //  معلومات الاتصال بقاعدة البيانات الخاصة بهذا المستأجر (تستخدم في نظام تعدد المستأجرين) تم وضعها nullable لأن بعض المستأجرين قد يستخدمون قاعدة بيانات مشتركة ولا يحتاجون إلى معلومات اتصال خاصة بهم

       

        private Tenant() { } // For EF Core

        public Tenant(string name, string storeName, string ownerUserId, int subscriptionPlanId)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant name is required.", nameof(name));
            if(string.IsNullOrWhiteSpace(storeName))
                throw new ArgumentException("Store name is required.", nameof(storeName));
            if(string.IsNullOrWhiteSpace(ownerUserId))
                throw new ArgumentException("Owner user id is required.", nameof(ownerUserId));
            Name = name;
            StoreName = storeName;
            OwnerUserId = ownerUserId;
            SubscriptionPlanId = subscriptionPlanId;
        }
    }
}
