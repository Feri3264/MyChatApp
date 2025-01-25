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
            throw new NotImplementedException();
        }

        public void RemoveMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
