using Chat.Data.Context;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Repository
{

    public class UserRepository
        (ChatContext _context) : IUserRepository
    {
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(UserModel user)
        {
            _context.Update(user);
        }

        public async Task DeleteAsync(UserModel user)
        {
            UserModel result = await GetUserAsync(user);
            user.isDelete = !user.isDelete;
        }

        public async Task<UserModel> GetUserAsync(UserModel user)
        {
            return await _context.Users.Include(f => f.Friends).FirstOrDefaultAsync(x => x == user);
        }

        public async Task<UserModel> GetByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await _context.Users
                .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.Username == emailOrUsername);
        }

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            return await _context.Users.Include(f => f.Friends).FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<UserModel>> GetByTakeAsync(int take, int skip)
        {
            return await _context.Users
                .OrderByDescending(u => u.UserId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(e => e.UserId == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username)
        {
            List<UserModel> users = await _context.Users
                 .Where(x => x.Username.Contains(username))
                 .Select(x => x)
                 .ToListAsync();
            return users;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
