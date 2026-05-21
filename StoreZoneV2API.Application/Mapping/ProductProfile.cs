using AutoMapper;
using StoreZoneV2API.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product , ProductsDto>();

        }
    }
}
