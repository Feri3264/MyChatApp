using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MessageViewComponent 
        (IMessageRepository _messageRepository) : ViewComponent
    {
        
        public IViewComponentResult Invoke(FriendModel friendship)
        {
            List<MessageModel> messages = _messageRepository.FindMessagesByFriendship(friendship)
                .OrderBy(m => m.MessageDate)
                .ToList(); 
            ViewData["MessageUserId"]= friendship.UserId;
            return View("MessageVC", messages);                       
        }
    }
}