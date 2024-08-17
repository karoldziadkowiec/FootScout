using FootScout.WebAPI.Entities;
using System.Threading.Tasks;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatById(int chatId);
        Task<int> GetChatIdBetweenUsers(string user1Id, string user2Id);
        Task CreateChat(Chat chat);
        Task DeleteChat(int chatId);
    }
}