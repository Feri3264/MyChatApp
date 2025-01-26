using System.Diagnostics;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
