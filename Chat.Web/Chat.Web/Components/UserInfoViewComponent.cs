using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        
        // === Getting Data --> Main View --> Main Action (HomeController) ===
        public IViewComponentResult Invoke(UserModel user)
        {            
            return View("UserInfoVC" , user);
        }
    }
}
