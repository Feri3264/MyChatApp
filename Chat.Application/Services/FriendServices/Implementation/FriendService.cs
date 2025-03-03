using Chat.Application.Services.FriendServices.Interface;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;

namespace Chat.Application.Services.FriendServices.Implementation;

public class FriendService
    (IFriendRepository friendRepository): IFriendService
{

    #region GetBy
    public async Task<FriendModel> GetByIdAsync(int userId, int friendId)
    {
        var friendship = await friendRepository.GetFriendshipAsync(userId, friendId);
        if (friendship == null)
            return null;

        return friendship;
    }
    #endregion

    #region Create
    public async Task CreateAsync(int userId, int friendId)
    {
        FriendModel friendship = new FriendModel
        {
            UserId = userId,
            FreindId = friendId
        };
        await friendRepository.AddFriendAsync(friendship);
    }
    #endregion

    #region Exists
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
    #endregion

    #region Delete
    public async Task DeleteAsync(int userId, int friendId)
    {
        var friendship = await GetByIdAsync(userId, friendId);
        friendRepository.Delete(friendship);
    }
    #endregion

    #region Save
    public async Task SaveChangesAsync()
    {
        await friendRepository.SaveChangesAsync();
    }
    #endregion

}