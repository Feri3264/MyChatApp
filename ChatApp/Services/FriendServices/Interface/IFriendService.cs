using DataLayer.Models;

namespace ChatApp.Services.FriendServices.Interface;

public interface IFriendService
{
    Task<FriendModel> GetByIdAsync(int userId , int friendId);

    Task DeleteAsync(int userId, int friendId);
    
    Task<bool> FriendshipExistsAsync(int userId , int friendId);
    
    Task CreateAsync(int userId, int friendId);
    
    Task SaveChangesAsync();
}