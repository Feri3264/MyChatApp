using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class FooterChatViewComponent : ViewComponent
    {
        IMessageRepository _messageRepository;
        public FooterChatViewComponent(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public IViewComponentResult Invoke(FriendModel friendship)
        {            
            return View("FooterChatVC" , friendship);
        }
    }
}
