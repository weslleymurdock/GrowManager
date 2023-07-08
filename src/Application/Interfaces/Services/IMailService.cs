using GrowManager.Application.Requests.Mail;
using System.Threading.Tasks;

namespace GrowManager.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}