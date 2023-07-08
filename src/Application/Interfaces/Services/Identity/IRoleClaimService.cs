using GrowManager.Application.Interfaces.Common;
using GrowManager.Application.Requests.Identity;
using GrowManager.Application.Responses.Identity;
using GrowManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowManager.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}