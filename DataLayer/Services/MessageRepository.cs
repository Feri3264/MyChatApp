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
    public class MessageRepository : IMessageRepository
    {

        ChatContext _context;

        public MessageRepository(ChatContext context)
        {
            _context = context;
        }

        public void AddMessage(MessageModel message)
        {
            _context.Messages.Add(message);            
        }

        public void RemoveMessage(int messageId)
        {
            MessageModel message = FindMessageById(messageId);
            _context.Messages.Remove(message);
        }

        public MessageModel FindMessageById(int messageId)
        {
            MessageModel message = _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
            return message;
        }

        public async Task<IEnumerable<MessageModel>> FindMessagesByFriendship(FriendModel friendship)
        {
            var messages = await _context.Messages.Where(m => m.Sender == friendship.UserId && m.Receiver == friendship.FreindId || m.Sender == friendship.FreindId && m.Receiver == friendship.UserId)
            .ToListAsync();
            return messages;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
