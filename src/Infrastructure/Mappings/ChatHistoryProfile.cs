using AutoMapper;
using GrowManager.Application.Interfaces.Chat;
using GrowManager.Application.Models.Chat;
using GrowManager.Infrastructure.Models.Identity;

namespace GrowManager.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}