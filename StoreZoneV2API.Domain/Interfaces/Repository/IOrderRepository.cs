using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
    public interface IOrderRepository
    {
        Task<bool> HasActiveOrderAsync(int productId);
    }
}
