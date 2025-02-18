using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
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
