using Chat.Domain.Models;

namespace Chat.Application.Services.MessageServices.Interface;

public interface IMessageService
{

    #region GetBy
    Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship);

    Task<MessageModel> GetByIdAsync(int id);
    #endregion

    #region Create
    Task CreateAsync(MessageModel message);
    #endregion

    #region Delete
    Task DeleteAsync(int id);
    #endregion

    #region Save
    Task SaveChangesAsync();
    #endregion    

}