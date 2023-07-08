using AutoMapper;
using GrowManager.Application.Features.Brands.Commands.AddEdit;
using GrowManager.Application.Features.Brands.Queries.GetAll;
using GrowManager.Application.Features.Brands.Queries.GetById;
using GrowManager.Domain.Entities.Catalog;

namespace GrowManager.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}