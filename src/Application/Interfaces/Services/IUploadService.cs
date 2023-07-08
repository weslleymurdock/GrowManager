using GrowManager.Application.Requests;

namespace GrowManager.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}