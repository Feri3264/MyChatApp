using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MessageViewComponent 
        (IMessageRepository _messageRepository) : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync(FriendModel friendship)
        {
            List<MessageModel> messages = await _messageRepository.FindMessagesByFriendship(friendship)
                .OrderBy(m => m.MessageDate)
                .ToList(); 
            ViewData["MessageUserId"]= friendship.UserId;
            return View("MessageVC", messages);                       
        }
    }
}