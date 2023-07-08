using AutoMapper;
using GrowManager.Application.Features.DocumentTypes.Commands.AddEdit;
using GrowManager.Application.Features.DocumentTypes.Queries.GetAll;
using GrowManager.Application.Features.DocumentTypes.Queries.GetById;
using GrowManager.Domain.Entities.Misc;

namespace GrowManager.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}