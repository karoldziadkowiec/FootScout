using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Services.Classes
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _dbContext;

        public ChatService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Chat> GetChatById(int chatId)
        {
            return await _dbContext.Chats
                .Include(c => c.User1)
                .Include(c => c.User2)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<int> GetChatIdBetweenUsers(string user1Id, string user2Id)
        {
            var chatId = await _dbContext.Chats
                .Where(c => (c.User1Id == user1Id && c.User2Id == user2Id) || (c.User1Id == user2Id && c.User2Id == user1Id))
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return chatId;
        }

        public async Task CreateChat(Chat chat)
        {
            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChat(int chatId)
        {
            var chat = await _dbContext.Chats.FindAsync(chatId);

            if (chat == null)
                throw new ArgumentException($"No chat found with ID {chatId}");

            var messages = await _dbContext.Messages
                    .Where(m => m.ChatId == chatId)
                    .ToListAsync();
            _dbContext.Messages.RemoveRange(messages);

            _dbContext.Chats.Remove(chat);
            await _dbContext.SaveChangesAsync();
        }
    }
}