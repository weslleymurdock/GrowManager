using AutoMapper;
using GrowManager.Application.Features.Documents.Commands.AddEdit;
using GrowManager.Application.Features.Documents.Queries.GetById;
using GrowManager.Domain.Entities.Misc;

namespace GrowManager.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}