using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
