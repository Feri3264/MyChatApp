using DataLayer.Models;


namespace DataLayer.Repository
{
    public interface IMessageRepository
    {
        public Task AddMessageAsync(MessageModel message);
        public void Delete(MessageModel message);
        public Task<MessageModel> GetByIdAsync(int messageId);
        public Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship);
        public Task SaveChangesAsync();
    }
}
