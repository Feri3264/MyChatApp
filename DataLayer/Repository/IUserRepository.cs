using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllUsers();
        public void AddUser(UserModel user);
        public void UpdateUser(UserModel user);
        public void RemoveUser(UserModel user);
        public void RemoveUser(int userId);        
        public UserModel FindUser(UserModel user);
        public UserModel FindUserByUsername(string username);
        public UserModel FindUserById(int? userId);
        public bool UserExists(int id);
        public void SaveChanges();
    }
}
