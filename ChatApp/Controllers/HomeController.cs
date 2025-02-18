using ChatApp.Services.UserServices.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController 
        (IUserService UserService) : Controller
    {

        //=== Getting Data From Login Page ===
        [HttpGet("/Home/{username}")]
        public async Task<IActionResult> Main(string username)
        {         
            UserModel user = await UserService.GetByUsernameAsync(username);
            return View(user);
        }

    }
}
