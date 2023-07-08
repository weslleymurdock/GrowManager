using GrowManager.Application.Interfaces.Common;
using GrowManager.Application.Requests.Identity;
using GrowManager.Application.Responses.Identity;
using GrowManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace GrowManager.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}