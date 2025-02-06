using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MessageViewComponent : ViewComponent
    {

        IMessageRepository _messageRepository;        
        public MessageViewComponent(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
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