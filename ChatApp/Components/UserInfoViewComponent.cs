using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
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
