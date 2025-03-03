using Chat.Application.Services.UserServices.Implementation;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]   
    public class EditProfileController (IUserService userService) : Controller
    {

        #region Edit
        [HttpGet("/EditProfile")]
        public async Task<IActionResult> Edit()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await userService.GetForEditProfile(userId);
            return View("EditProfile", user);
        }

        [HttpPost("/EditProfilePost")]
        public async Task<IActionResult> EditOnPost(EditProfileDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await userService.Update(model);
            await userService.SaveChangesAsync();

            var user = await userService.GetByIdAsync(model.UserId);
            return RedirectToAction("Index", "Home", new { username = user.Username });
        }
        #endregion

        #region ChangePassword
        [HttpGet("/ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost("/ChangePasswordPost")]
        public async Task<IActionResult> ChangePasswordPost(string password)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await userService.GetByIdAsync(userId);

            await userService.ChangePassword(userId , password);
            await userService.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { username = user.Username });
        }
        #endregion
    }
}
