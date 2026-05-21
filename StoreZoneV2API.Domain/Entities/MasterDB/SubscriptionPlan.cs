using StoreZoneV2API.Domain.Common;
using StoreZoneV2API.Domain.Enums;
using StoreZoneV2API.Domain.ValueObjects;
using System.Globalization;

namespace StoreZoneV2API.Domain.Entities.MasterDB
{
    public class SubscriptionPlan : BaseEntity
    {
        public string  Name { get; private set; } = string.Empty; // Free , Professonal , Commercial(Enterprice)
        public string DiscriptionInfo { get; set; } = string.Empty; // معلومات عن الباقة ومن الذي يستخدمها
        public Money MonthlyPrice { get; private set; } = null!;
        public Discount YearlyDiscountPercent { get; private set; } = null!; // ضبط الخصم من خلال شاشة التحكم 
        public decimal YearlyPrice => MonthlyPrice.Amount * 12 * (1 - (YearlyDiscountPercent.Percent / 100)); // حساب نسبة

        // vlaue objects for limits and features
        public PlaneLimits Limits { get; private set; } = null!; //  هذا يمثل حدود الاشتراك مثل عدد المنتجات وعدد المستخدمين وغيرها من الميزات التي يمكن أن تكون مختلفة بين الخطط المختلفة
        public PlanFeatures Features { get; private set; } = null!; //  هذا يمثل ميزات الاشتراك مثل استخدام دومين خاص، توصيات ذكية، تقارير متقدمة، دعم مميز
        public TenantDatabaseMode DatabaseMode { get; private set; } // هل يستخدم قاعدة بيانات مشتركة أم قاعدة بيانات مخصصة لكل مستأجر

        //  العلاقات مع المستأجرين الذين يستخدمون هذا الاشتراك
        public List<Tenant> Tenants { get; private set; } = new();//  قائمة المتاجر التي تستخدم هذا الاشتراك نحتاج هذه لغرض التقارير والاحصاء

        private SubscriptionPlan()
        {
            // For EF Core
        }

       public SubscriptionPlan(string name, 
                               Money monthlyPrice, 
                               Discount yearlyDiscountPercent,                        
                               TenantDatabaseMode databaseMode ,
                               PlaneLimits limits, 
                               PlanFeatures features)
        {
            SetName(name);
            MonthlyPrice= monthlyPrice ?? throw new ArgumentNullException(nameof(monthlyPrice));
            YearlyDiscountPercent = yearlyDiscountPercent ?? throw new ArgumentNullException(nameof(yearlyDiscountPercent));
            DatabaseMode = databaseMode;
            Limits = limits ?? throw new ArgumentNullException(nameof(limits));
            Features = features ?? throw new ArgumentNullException(nameof(features));
            
        }

        



        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name cannot be null or empty.", nameof(name));
            if(name.Length > 100)
                throw new ArgumentException(nameof(name) ,  " cannot exceed 100 characters.");
            Name = name.Trim();
        }

        public void ChangMonthlyPrice(Money newPrice)
        {
            MonthlyPrice = newPrice ?? throw new ArgumentNullException(nameof(newPrice) , "New price cannot be null.");
        }

        public void changeYearlyDiscount(Discount newDiscount)
        {
            YearlyDiscountPercent = newDiscount ?? throw new ArgumentNullException(nameof(newDiscount) , "New discount cannot be null.");
        }
        
        public void ChangeLimits(PlaneLimits newLimits)
        {
            Limits = newLimits ?? throw new ArgumentNullException(nameof(newLimits), "New limits cannot be null.");

        }

        public void ChangeFeatures(PlanFeatures newFeatures)
        {
            Features = newFeatures ?? throw new ArgumentNullException(nameof(newFeatures), "New features cannot be null.");
        }

        public void ChangeDatabaseMode(TenantDatabaseMode newDatabaseMode)
        {
            DatabaseMode = newDatabaseMode;
        }


        
    }
}