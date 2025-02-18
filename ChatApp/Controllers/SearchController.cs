using ChatApp.Services.FriendServices.Interface;
using ChatApp.Services.UserServices.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.Controllers
{
    [Authorize]
    public class SearchController 
        (IFriendService FriendService, IUserService UserService) : Controller
    {


        // === Getting Data From User Page ===
        [HttpGet("/search/{userId}")]
        public IActionResult Search(int userId)
        {            
            TempData["userId"] = userId;
            return View();
        }


        // === Getting Data From Search Page ===
        [HttpGet("/AddFriend")]
        public async Task<IActionResult> AddFriend(int friendId)
        {
            int userId = (int)TempData["userId"];
            UserModel user = await UserService.GetByIdAsync(userId);

            if (await FriendService.FriendshipExistsAsync(userId, friendId))
            {
                return Redirect($"/Home/{user.Username}");
            }

            await FriendService.CreateAsync(userId, friendId);
            await FriendService.SaveChangesAsync();
            
            return Redirect($"/Home/{user.Username}");
        }


        // === Getting Data --> Ajax (Search View) ===
        [HttpPost]
        public IActionResult ReturnSearchedFriendsViewComponent(string data)
        {
            return ViewComponent("SearchedFriends", data);
        }
    }
}