using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class HomeController 
        (IUserService UserService , IFriendService FriendService) : Controller
    {

        //=== Getting Data From Login Page ===
        [HttpGet("/Home/{username}")]
        public async Task<IActionResult> Main(string username)
        {         
            UserModel user = await UserService.GetByUsernameAsync(username);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFriend(int userId , int friendId)
        {
            await FriendService.DeleteAsync(userId, friendId);
            await FriendService.SaveChangesAsync();
            
            var user = await UserService.GetByIdAsync(userId);
            return Redirect($"/Home/{user.Username}");
        }
    }
}
