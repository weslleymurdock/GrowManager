using GrowManager.Application.Interfaces.Common;

namespace GrowManager.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}