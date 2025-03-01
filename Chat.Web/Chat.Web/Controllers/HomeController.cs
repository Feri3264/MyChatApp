using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class HomeController 
        (IUserService UserService , IFriendService FriendService) : Controller
    {

        //=== Getting Data From Login Page ===
        [HttpGet]
        public async Task<IActionResult> Index(string username)
        {         
            UserModel user = await UserService.GetByEmailOrUsernameAsync(username);
            if(user == null)
                return NotFound();
            
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFriend(int userId , int friendId)
        {
            await FriendService.DeleteAsync(userId, friendId);
            await FriendService.SaveChangesAsync();
            
            var user = await UserService.GetByIdAsync(userId);
            return RedirectToAction("Index" , new { username = user.Username });
        }
        
        [HttpGet]
        public async Task<IActionResult> EditProfile(int userId)
        {
            var userViewModel = await UserService.GetForEditProfile(userId);
            if(userViewModel == null)
                return NotFound();
            
            return View(userViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDTO model)
        {
            var user = await UserService.GetByIdAsync(model.UserId);            
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await UserService.Update(model);
            await UserService.SaveChangesAsync();
            return RedirectToAction("Index" , new { username = user.Username });
        }
    }
}
