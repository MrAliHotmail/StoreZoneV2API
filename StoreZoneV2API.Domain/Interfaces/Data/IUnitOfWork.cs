using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
     public interface IUnitOfWork : IDisposable
    {
        // مخازن البيانات (Repositories)
        IProductRepository ProductRepository { get; }
        ICategoreyRespository CategoreyRespository { get; }

        // حفظ جميع التغييرات معاً
        Task<int> SaveChangesAsync();


        // إدارة العمليات (Transactions)
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();


    }
}
