using Chat.Data.Context;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Repository
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
            return await _context.Friends.Include(m => m.Messages).FirstOrDefaultAsync(f => f.UserId == userId && f.FreindId == friendId);         
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
