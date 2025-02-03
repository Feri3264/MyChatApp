using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(UserModel user)
        {            
            return View("UserInfoVC" , user);
        }
    }
}
