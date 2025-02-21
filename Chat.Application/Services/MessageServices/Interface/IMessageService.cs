using Chat.Domain.Models;

namespace Chat.Application.Services.MessageServices.Interface;

public interface IMessageService
{
    Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship);
    
    Task<MessageModel> GetByIdAsync(int id);
    
    Task CreateAsync(MessageModel message);
    
    Task DeleteAsync(int id);
    
    Task SaveChangesAsync();
}