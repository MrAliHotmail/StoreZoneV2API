using Microsoft.EntityFrameworkCore.Storage;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;
using StoreZoneV2API.Infrastructure.Repositories;


namespace StoreZoneV2API.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork , IDisposable 
    {
        private readonly ApplicationDbContext _dbContext;
        public IDbContextTransaction? _transaction;      
        public IProductRepository Products { get;   }
        public ICategoryRespository Categories { get;  }
        public UnitOfWork(ApplicationDbContext  dbContext) 
        { 

            _dbContext =  dbContext;
            Products = new ProductRepository(dbContext);
            Categories = new CategoryRepository(dbContext);

        } 
       

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

              _transaction = await _dbContext.Database.BeginTransactionAsync();
            
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                return;
            await _dbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _dbContext.DisposeAsync();    
            _transaction = null;    

           
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _transaction?.DisposeAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                return;
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();  
            _transaction= null; 
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }

}
