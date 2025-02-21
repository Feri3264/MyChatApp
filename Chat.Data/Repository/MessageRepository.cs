using Chat.Data.Context;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Repository
{
    public class MessageRepository
        (ChatContext _context): IMessageRepository
    {

        public async Task AddMessageAsync(MessageModel message)
        {
             await _context.Messages.AddAsync(message);            
        }

        public void Delete(MessageModel message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<MessageModel> GetByIdAsync(int messageId)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == messageId);
        }

        public async Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship)
        {
            return await _context.Messages
                .Where(m => m.Sender == friendship.UserId && m.Receiver == friendship.FreindId || m.Sender == friendship.FreindId && m.Receiver == friendship.UserId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
