using ChatApp.Services.FriendServices.Interface;
using DataLayer.Models;
using DataLayer.Repository;

namespace ChatApp.Services.FriendServices.Implementation;

public class FriendService
    (IFriendRepository friendRepository): IFriendService
{
    public async Task<FriendModel> GetByIdAsync(int userId, int friendId)
    {
        return await friendRepository.GetFriendshipAsync(userId, friendId);
    }

    public async Task DeleteAsync(int userId, int friendId)
    {
        var friendship = await GetByIdAsync(userId, friendId);
        friendRepository.Delete(friendship);
    }

    public async Task<bool> FriendshipExistsAsync(int userId, int friendId)
    {
        var friendship = await GetByIdAsync(userId, friendId);
        if (friendship != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task CreateAsync(int userId, int friendId)
    {
        FriendModel friendship = new FriendModel
        {
            UserId = userId,
            FreindId = friendId
        };
        await friendRepository.AddFriendAsync(friendship);
    }

    public async Task SaveChangesAsync()
    {
        await friendRepository.SaveChangesAsync();
    }
}