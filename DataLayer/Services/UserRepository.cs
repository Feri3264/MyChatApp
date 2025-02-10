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

        private readonly ChatContext _context;

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
            UserModel result = FindUser(user);
            _context.Users.Remove(result);
        }

        public void RemoveUser(int userId)
        {
            UserModel user = FindUserById(userId);
            _context.Users.Remove(user);
        }

        public UserModel FindUser(UserModel user)
        {
            UserModel FoundUser = _context.Users.FirstOrDefault(user);
            return FoundUser;
        }

        public UserModel FindUserByUsername(string username)
        {
            UserModel user = _context.Users.Include(f => f.Friends).FirstOrDefault(u => u.Username == username);
            return user;
        }

        public UserModel FindUserById(int userId)
        {
            UserModel user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return user;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
