using System;
using System.Linq;
using AutoMapper;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUserForRegister, AppUser>();
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.AppUserRoles.Select(x=> x.Role)));

            CreateMap<AppRole, AppRoleDTO>();
            CreateMap<AppUserForUpdate, AppUser>();
            CreateMap<AppUserForRegister, AppUser>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryForCreate, Category>();
            CreateMap<CategoryForUpdate, Category>();

            CreateMap<ContactUsForCreate, ContactUs>();
            CreateMap<ContactUsForUpdate, ContactUs>();

            CreateMap<PaymentType, PaymentTypeDTO>();
            CreateMap<OrderStatus, OrderStatusDTO>();

            CreateMap<SalesOrderForCreate, SalesOrder>();
          

            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(s => s.ProductPhotos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.ProductPhotos));

            CreateMap<ProductPhoto, ProductPhotoDTO>();
            CreateMap<ProductForCreate, Product>();
            CreateMap<ProductForUpdate, Product>();

            CreateMap<DeliveryType, DeliveryTypeDTO>();
            CreateMap<SalesOrder, SalesOrderDTO>()
                .ForMember(x => x.Amount, opt => opt.MapFrom(y => y.Price * y.Quantity))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(y => y.Product.ProductName));


            CreateMap<PaymentDetailForCreate, PaymentDetail>();
        }
    }
}
