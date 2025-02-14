using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Services;

namespace ChatApp.Controllers
{
    public class AccountController
        (IUserRepository _userRepository, ChatContext _context) : Controller
    {        


        #region Register
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(CreateUserViewModel user)
        {            
            user.isAdmin = false;
            if (ModelState.IsValid)
            {                
                var RegisteredUser = new UserModel
                {
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    isAdmin = user.isAdmin,
                    Picture = ProfilePicure.Add(user)
                };
                _userRepository.AddUser(RegisteredUser);
                _userRepository.SaveChanges();
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
        public IActionResult Login(string email, string password)
        {
            UserModel user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return View();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier , user.UserId.ToString() , ClaimValueTypes.Integer32),
                new Claim (ClaimTypes.Name , user.Username, ClaimValueTypes.String),
                new Claim (ClaimTypes.GivenName , user.Name , ClaimValueTypes.String),
                new Claim ("Admin" , user.isAdmin.ToString() , ClaimValueTypes.Boolean)
            };

            var Identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(Identity);
            HttpContext.SignInAsync(principal);


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