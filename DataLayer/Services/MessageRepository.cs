using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
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
