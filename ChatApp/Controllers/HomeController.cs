using System.Diagnostics;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController 
        (ChatContext _context) : Controller
    {
        
        [HttpGet("/{username}")]
        public IActionResult Main(string username)
        {          
            UserModel user = _context.Users.Include(f => f.Friends).FirstOrDefault(u => u.Username == username);
            return View(user);
        }
    }
}
