using System.Security.Claims;
using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class SearchController 
        (IFriendService FriendService, IUserService UserService) : Controller
    {
        
        // === Getting Data From User Page ===
        [HttpGet]
        public IActionResult Search(int userId)
        {           
            TempData["UserId"] = userId;
            return View();
        }


        // === Getting Data From Search Page ===
        //[HttpGet("/AddFriend")]
        [HttpGet]
        public async Task<IActionResult> AddFriend(int friendId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            UserModel user = await UserService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            if (await FriendService.FriendshipExistsAsync(userId , friendId))
            {
                return RedirectToAction("Index" , "Home" , new { username = user.Username });
            }

            await FriendService.CreateAsync(userId, friendId);
            await FriendService.SaveChangesAsync();
            
            return RedirectToAction("Index" , "Home" , new { username = user.Username });
        }


        // === Getting Data --> Ajax (Search View) ===
        [HttpPost]
        public IActionResult ReturnSearchedFriendsViewComponent(string data)
        {
            return ViewComponent("SearchedFriends", data);
        }
    }
}