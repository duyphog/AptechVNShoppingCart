using System;
using AutoMapper;
using Entities.Models;
using Entities.Models.RequestModels;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegister, AppUser>();
                //.ForMember(dest => dest.CreateDate, (src) => DateTime.Now);

        }
    }
}
