using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }

        public bool IsActive { get; protected set; } = true;
        public bool IsDeleted { get; protected set; } = false;

        public void softDelete()
        {
            IsDeleted = true;
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
