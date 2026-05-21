using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Entities;
using StoreZoneV2API.Domain.Interfaces.Services;
using StoreZoneV2API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Application.Services
{

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public Task<bool> HasActiveOrderAsync(int productId)
        //{
        //   // return _dbContext.OrderItems.AnyAsync(x => x.ProductId == productId && x.Status);
        //}
    }
}
