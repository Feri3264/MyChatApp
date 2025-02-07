using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email , string password)
        {
            UserModel user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return Redirect($"/{user.Username}");
            }
            return View();
        }
    }
}
