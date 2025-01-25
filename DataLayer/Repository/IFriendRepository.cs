using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IFriendRepository
    {      
        public void AddFriend(FriendModel friend);
        public void RemoveFriendship(int friendId, int userId);
        public FriendModel FindFriendship(int friendId , int userId);
        public void SaveChanges();
    }
}
