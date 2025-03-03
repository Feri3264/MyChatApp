using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Components
{
    public class MainFriendsViewComponent 
        (IUserService UserService , IFriendService FriendService) : ViewComponent
    {       

        // === Getting Data --> Main View --> Main Action (HomeController) ===
        public async Task<IViewComponentResult> InvokeAsync(UserModel model)
        {
            ViewData["UserId"] = model.UserId;
            List<UserModel> result = new List<UserModel>();


            if (model.Friends == null)
                return View("MainFriendsVC" , result);


            foreach (var item in model.Friends)
            {
                var friends = await UserService.GetByIdAsync(item.FreindId);
                if (friends.isDelete == false)
                {
                    result.Add(friends);
                }
                continue;
            }

            return View("MainFriendsVC" , result);
        }
    }
}


