using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class AccountController
        (IUserService UserService) : Controller
    {        


        #region Register
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO user)
        {            
            if (ModelState.IsValid)
            {                
                await UserService.RegisterAsync(user);
                await UserService.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            else
            {
                return View(user);
            }
        }
        #endregion


        #region Login

        //=== Main Page ===
        // /account/login
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!await UserService.UserExistsAsync(email, password))
            {
                ViewData["UserNotFound"] = "There is No such an account.";
                return View();
            }
            
            UserModel user = await UserService.GetByEmailAsync(email);
            var principal = UserService.PricipalUser(user);
            await HttpContext.SignInAsync(principal);
            
            return RedirectToAction("Index" , "Home" , new {username = user.Username});
        }
        #endregion


        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion


        #region Logout
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            return NotFound();
        }
        #endregion

    }
}