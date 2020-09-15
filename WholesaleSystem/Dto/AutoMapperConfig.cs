using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WholesaleSystem.Models;

namespace WholesaleSystem.Dto
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Inventory, InventoryDto>();
            CreateMap<PicturePath, PicturePathDto>();
            CreateMap<ProductType, ProductTypeDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.InventoryProductTypes.Count));
        }
    }
}
