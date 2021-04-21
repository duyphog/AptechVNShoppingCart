﻿using System;
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
            CreateMap<AppUser, AppUserDTO>();

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
            CreateMap<SalesOrderDetailForCreate, SalesOrderDetail>();
            CreateMap<SalesOrder, SalesOrderDTO>().
                ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.SalesOrderDetails.Sum(x => x.Quantity*x.Price)));
            CreateMap<SalesOrderDetail, SalesOrderDetailDTO>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Quantity * s.Price));

            CreateMap<Product, ProductDTO>().
                ForMember(d => d.PhotoUrl, opt => opt.MapFrom(s => s.ProductPhotos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<ProductPhoto, ProductPhotoDTO>();
            CreateMap<ProductForCreate, Product>();
            CreateMap<ProductForUpdate, Product>();

        }
    }
}
