using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public class FriendRepository
        (ChatContext _context): IFriendRepository
    {

        public async Task AddFriendAsync(FriendModel friend)
        {
            await _context.Friends.AddAsync(friend);
        }        

        public void Delete(FriendModel friendship)
        {
            _context.Friends.Remove(friendship);
        }

        public async Task<FriendModel> GetFriendshipAsync(int userId, int friendId)
        {
            return await _context.Friends.Include(m => m.Messages).FirstOrDefaultAsync(f => f.UserId == friendId && f.FreindId == userId);         
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
