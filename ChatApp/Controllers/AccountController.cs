using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
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
