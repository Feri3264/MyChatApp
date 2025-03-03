using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class HomeController 
        (IUserService UserService , IFriendService FriendService) : Controller
    {

        #region Index
        //=== Getting Data From Login Page ===
        [HttpGet]
        public async Task<IActionResult> Index(string username)
        {
            UserModel user = await UserService.GetByEmailOrUsernameAsync(username);
            if (user == null)
                return NotFound();

            return View(user);
        }
        #endregion

        #region Delete Friend
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(int userId, int friendId)
        {
            await FriendService.DeleteAsync(userId, friendId);
            await FriendService.SaveChangesAsync();

            var user = await UserService.GetByIdAsync(userId);
            return RedirectToAction("Index", new { username = user.Username });
        }
        #endregion

    }
}
