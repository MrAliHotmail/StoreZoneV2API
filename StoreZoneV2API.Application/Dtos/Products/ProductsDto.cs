using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Application.Dtos.Products
{
    public class ProductsDto : BaseEntity
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public string imagesUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool InStock { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
