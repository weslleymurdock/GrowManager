using GrowManager.Application.Features.Products.Commands.AddEdit;
using GrowManager.Application.Features.Products.Queries.GetAllPaged;
using GrowManager.Application.Requests.Catalog;
using GrowManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace GrowManager.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}