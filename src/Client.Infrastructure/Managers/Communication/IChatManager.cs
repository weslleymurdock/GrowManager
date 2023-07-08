using GrowManager.Application.Interfaces.Chat;
using GrowManager.Application.Models.Chat;
using GrowManager.Application.Responses.Identity;
using GrowManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowManager.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}