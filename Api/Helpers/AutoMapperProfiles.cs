using System;
using System.Linq;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.RequestModels;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegister, AppUser>();
            //.ForMember(dest => dest.CreateDate, (src) => DateTime.Now);
            CreateMap<AppUser, AppUserDTO>();
                //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.AppUserRoles.Select(ur => ur.Role).AsEnumerable()));

            CreateMap<AppRole, AppRoleDTO>();
        }
    }
}
