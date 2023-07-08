using GrowManager.Application.Features.Dashboards.Queries.GetData;
using GrowManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace GrowManager.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}