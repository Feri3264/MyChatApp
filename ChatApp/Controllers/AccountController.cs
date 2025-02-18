using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ChatApp.ViewModels;
using ChatApp.Services.UserServices.Interface;

namespace ChatApp.Controllers
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
        public async Task<IActionResult> Register(CreateUserViewModel user)
        {            
            user.isAdmin = false;
            if (ModelState.IsValid)
            {                
                await UserService.CreateAsync(user);
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
                return View();
            }
            
            UserModel user = await UserService.GetByEmailAsync(email);
            var principal = UserService.PricipalUser(user);
            await HttpContext.SignInAsync(principal);
            
            return Redirect($"/Home/{user.Username}");
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