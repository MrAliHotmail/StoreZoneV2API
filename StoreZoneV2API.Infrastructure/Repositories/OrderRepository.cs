using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Extentions;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> HasActiveOrderAsync(int productId)
        {    
            return await _dbContext.OrderItems.AnyAsync(x => x.ProductId == productId && x.Status.IsActiveOrders());  
        }
    }
}
