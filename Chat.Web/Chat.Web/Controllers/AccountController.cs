using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Chat.Domain.Enum;

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
                var result = await UserService.RegisterAsync(user);
                switch(result)
                {
                    case RegisterUserResultEnum.Success:
                        await UserService.SaveChangesAsync();
                        return RedirectToAction("Login");

                    case RegisterUserResultEnum.UsernameAlreadyExists:
                        ModelState.AddModelError("Username", "Username Alreay Exists");
                        break;

                    case RegisterUserResultEnum.EmailAlreadyExists:
                        ModelState.AddModelError("Email", "Email Alreay Exists");
                        break;

                    case RegisterUserResultEnum.PasswordNotValid:
                        ModelState.AddModelError("Password", "Password Must Contains 8 Characters");
                        break;
                }
                return View(user);

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
            {
                ModelState.AddModelError("Password", "User Not Found");
                return View(model);
            }                                    

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
        {chrome://vivaldi-webui/startpage?section=Speed-dials&background-color=#1e201e
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