using DataLayer.Models;

namespace DataLayer.Repository
{
    public interface IFriendRepository
    {      
        public Task AddFriendAsync(FriendModel friend);
        public void Delete(FriendModel friendship);
        public Task<FriendModel> GetFriendshipAsync(int userId, int friendId);
        public Task SaveChangesAsync();
    }
}
