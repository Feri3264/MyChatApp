﻿using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class FriendRepository : IFriendRepository
    {
        
        ChatContext _context;
        public FriendRepository(ChatContext context)
        {
            _context = context;
        }

        public void AddFriend(FriendModel friend)
        {
            _context.Friends.Add(friend);
        }        

        public void RemoveFriendship(int friendId, int userId)
        {
            FriendModel friendship = FindFriendship(friendId, userId);
            _context.Friends.Remove(friendship);
        }

        public FriendModel FindFriendship(int friendId, int userId)
        {
            FriendModel friendship = _context.Friends.FirstOrDefault(f => f.UserId == userId && f.FreindId == friendId);
            if (friendship == null)
            {
                throw new NullReferenceException();
            }           
            return friendship;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
