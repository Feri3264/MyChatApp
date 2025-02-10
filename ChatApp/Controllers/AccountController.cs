﻿using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Web.Helpers;
using ChatApp.ViewModels;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {

        ChatContext _context;
        IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository , ChatContext context)
        {
            _context = context;
            _userRepository = userRepository;
        }


        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {            
            if (ModelState.IsValid)
            {                
                var RegisteredUser = new UserModel
                {
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    isAdmin = false,
                    Picture = AddProfilePic(user)
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

  
        public string AddProfilePic(RegisterViewModel user)
        {
            string fileName = Guid.NewGuid().ToString() + user.Username.ToString() + Path.GetExtension(user.ProfilePicture.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "ProfilePicture",
                fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                user.ProfilePicture.CopyTo(stream);
            }
            return fileName;
        }
        #endregion


        #region Login
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
                new Claim (ClaimTypes.GivenName , user.Name , ClaimValueTypes.String)
            };

            var Identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(Identity);
            HttpContext.SignInAsync(principal);

            return Redirect($"/{user.Username}");
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