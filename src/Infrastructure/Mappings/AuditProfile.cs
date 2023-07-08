using AutoMapper;
using GrowManager.Application.Responses.Audit;
using GrowManager.Infrastructure.Models.Audit;

namespace GrowManager.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}