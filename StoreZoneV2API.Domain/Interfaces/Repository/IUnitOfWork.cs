using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
     public interface IUnitOfWork : IDisposable
    {
        // مخازن البيانات (Repositories)
        IProductRepository Products { get; }
        ICategoryRespository Categories { get; }

        // حفظ جميع التغييرات معاً
        Task<int> SaveChangesAsync();

        // إدارة العمليات (Transactions)
        // خصم المبلغ من رصيد العميل -
        // خصم الكمية من المخزون-(Inventory -
        // تحديث حالة الطلب إلى "تم الدفع"
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        
    }
}
