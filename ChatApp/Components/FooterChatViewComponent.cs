using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class FooterChatViewComponent 
        (IMessageRepository _messageRepository) : ViewComponent
    {


        public IViewComponentResult Invoke(FriendModel friendship)
        {            
            return View("FooterChatVC" , friendship);
        }
    }
}
