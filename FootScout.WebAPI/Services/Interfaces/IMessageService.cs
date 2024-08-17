using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesForChat(int chatId);
        Task<Message> SendMessage(MessageSendDTO dto);
        Task DeleteMessage(int messageId);
    }
}