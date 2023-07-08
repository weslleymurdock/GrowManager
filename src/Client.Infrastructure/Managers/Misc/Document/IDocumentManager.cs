using GrowManager.Application.Features.Documents.Commands.AddEdit;
using GrowManager.Application.Features.Documents.Queries.GetAll;
using GrowManager.Application.Features.Documents.Queries.GetById;
using GrowManager.Application.Requests.Documents;
using GrowManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace GrowManager.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}