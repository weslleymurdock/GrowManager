using GrowManager.Application.Requests.Identity;
using GrowManager.Application.Responses.Identity;
using GrowManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowManager.Client.Infrastructure.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}