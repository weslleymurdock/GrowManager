using AutoMapper;
using GrowManager.Application.Requests.Identity;
using GrowManager.Application.Responses.Identity;

namespace GrowManager.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}