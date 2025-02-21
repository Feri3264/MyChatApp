using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Components
{
    public class FooterChatViewComponent : ViewComponent
    {
        // === Getting Data --> Chat View --> Chat Action (ChatController) ===
        public IViewComponentResult Invoke(FriendModel friendship)
        {            
            return View("FooterChatVC" , friendship);
        }
    }
}
