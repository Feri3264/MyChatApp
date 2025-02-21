using Chat.Domain.Models;

namespace Chat.Application.Services.FriendServices.Interface;

public interface IFriendService
{
    Task<FriendModel> GetByIdAsync(int userId , int friendId);

    Task DeleteAsync(int userId, int friendId);
    
    Task DeleteAllFriendsAsync(IEnumerable<FriendModel> friends);
    
    Task<bool> FriendshipExistsAsync(int userId , int friendId);
    
    Task CreateAsync(int userId, int friendId);
    
    Task SaveChangesAsync();
}