using AutoMapper;
using GrowManager.Application.Features.Products.Commands.AddEdit;
using GrowManager.Domain.Entities.Catalog;

namespace GrowManager.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}