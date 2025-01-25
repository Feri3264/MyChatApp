using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IMessageRepository
    {
        public void AddMessage(MessageModel message);
        public void RemoveMessage(int messageId);
        public void SaveChanges();
    }
}
