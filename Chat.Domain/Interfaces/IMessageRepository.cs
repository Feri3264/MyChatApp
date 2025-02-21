using Chat.Domain.Models;

namespace Chat.Domain.Interfaces
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
