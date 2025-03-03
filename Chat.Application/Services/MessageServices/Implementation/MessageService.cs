using Chat.Application.Services.MessageServices.Interface;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;

namespace Chat.Application.Services.MessageServices.Implementation;

public class MessageService
    (IMessageRepository messageRepository): IMessageService
{

    #region GetBy
    public async Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship)
    {
        var messages = await messageRepository.GetByFriendshipAsync(friendship);
        if (messages == null)
            return null;

        return messages;
    }

    public async Task<MessageModel> GetByIdAsync(int id)
    {
        var message = await messageRepository.GetByIdAsync(id);
        if (message == null)
            return null;

        return message;
    }
    #endregion

    #region Create
    public async Task CreateAsync(MessageModel message)
    {
        await messageRepository.AddMessageAsync(message);
    }
    #endregion

    #region Delete
    public async Task DeleteAsync(int id)
    {
        var message = await messageRepository.GetByIdAsync(id);
        messageRepository.Delete(message);
    }
    #endregion

    #region Save
    public async Task SaveChangesAsync()
    {
        await messageRepository.SaveChangesAsync();
    }
    #endregion

}