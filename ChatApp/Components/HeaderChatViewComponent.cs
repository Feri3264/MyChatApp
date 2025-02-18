using ChatApp.Services.UserServices.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class HeaderChatViewComponent 
        (IUserService UserService) : ViewComponent
    {

        // === Getting Data --> Chat Page --> Chat Action (ChatController) ===
        public async Task<IViewComponentResult> InvokeAsync(int friendId)
        {
            UserModel friend = await UserService.GetByIdAsync(friendId);
            return View("HeaderChatVC" , friend);
        }
    }
}
