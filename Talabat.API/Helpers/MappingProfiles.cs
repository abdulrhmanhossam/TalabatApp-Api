using AutoMapper;
using Talabat.API.Dtos;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Helpers
{
    public class MappingProfiles : Profile
    {
        // mapping context to Dto 
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();

            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
