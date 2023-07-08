using AutoMapper;
using GrowManager.Application.Responses.Identity;
using GrowManager.Infrastructure.Models.Identity;

namespace GrowManager.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}