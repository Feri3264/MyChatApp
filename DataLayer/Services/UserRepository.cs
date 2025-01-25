using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{

    public class UserRepository : IUserRepository
    {

        ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }


        public void AddUser(UserModel user)
        {
            _context.Users.Add(user);
        }

        public void RemoveUser(UserModel user)
        {
            FindUser(user);
            _context.Users.Remove(user);
        }

        public void RemoveUser(int userId)
        {
            UserModel user = FindUserById(userId);
            _context.Users.Remove(user);
        }

        public UserModel FindUser(UserModel user)
        {
            UserModel FoundUser = _context.Users.FirstOrDefault(user);
            if (FoundUser == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return FoundUser;
            }
        }

        public UserModel FindUserById(int userId)
        {
            UserModel user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return user;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
