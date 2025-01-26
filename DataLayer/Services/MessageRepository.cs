using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (message == null)
            {
                throw new NullReferenceException();
            }
            return message;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
