using Chat.Domain.Models;

namespace Chat.Application.Services.FriendServices.Interface;

public interface IFriendService
{

    #region GetBy
    Task<FriendModel> GetByIdAsync(int userId, int friendId);
    #endregion

    #region Create
    Task CreateAsync(int userId, int friendId);
    #endregion

    #region Exists
    Task<bool> FriendshipExistsAsync(int userId, int friendId);
    #endregion

    #region Delete
    Task DeleteAsync(int userId, int friendId);
    #endregion

    #region Save
    Task SaveChangesAsync();
    #endregion

}