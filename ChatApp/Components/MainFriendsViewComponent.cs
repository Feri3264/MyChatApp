using ChatApp.Services.UserServices.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MainFriendsViewComponent 
        (IUserService UserService) : ViewComponent
    {       

        // === Getting Data --> Main View --> Main Action (HomeController) ===
        public async Task<IViewComponentResult> InvokeAsync(UserModel model)
        {
            ViewData["UserId"] = model.UserId;
            List<UserModel> users = new List<UserModel>();
            
            foreach (var item in model.Friends)
            {
                var result = await UserService.GetByIdAsync(item.UserId);
                users.Add(result);
            }

            return View("MainFriendsVC" , users);
        }
    }
}
