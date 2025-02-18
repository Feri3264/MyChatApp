using ChatApp.Services.MessageServices.Interface;
using DataLayer.Models;
using DataLayer.Repository;

namespace ChatApp.Services.MessageServices.Implementation;

public class MessageService
    (IMessageRepository messageRepository): IMessageService
{
    public async Task<IEnumerable<MessageModel>> GetByFriendshipAsync(FriendModel friendship)
    {
        return await messageRepository.GetByFriendshipAsync(friendship);
    }

    public async Task<MessageModel> GetByIdAsync(int id)
    {
        return await messageRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(MessageModel message)
    {
        await messageRepository.AddMessageAsync(message);
    }

    public async Task DeleteAsync(int id)
    {
        var message = await messageRepository.GetByIdAsync(id);
        messageRepository.Delete(message);
    }

    public async Task SaveChangesAsync()
    {
        await messageRepository.SaveChangesAsync();
    }
}