using Chat.Domain.Models;

namespace Chat.Domain.Interfaces
{
    public interface IFriendRepository
    {      
        public Task AddFriendAsync(FriendModel friend);
        public void Delete(FriendModel friendship);
        public Task<FriendModel> GetFriendshipAsync(int userId, int friendId);
        public Task SaveChangesAsync();
    }
}
