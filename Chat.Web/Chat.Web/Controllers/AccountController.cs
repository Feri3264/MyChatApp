using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
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
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!await UserService.UserExistsAsync(model.EmailOrUsername, model.Password))
                ModelState.AddModelError("Password", "User Not Found");


            UserModel user = await UserService.GetByEmailOrUsernameAsync(model.EmailOrUsername);
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