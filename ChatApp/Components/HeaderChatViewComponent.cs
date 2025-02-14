using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class HeaderChatViewComponent 
        (IUserRepository _userRepository) : ViewComponent
    {

        // === Getting Data --> Chat Page --> Chat Action (ChatController) ===
        public IViewComponentResult Invoke(int friendId)
        {
            UserModel friend = _userRepository.FindUserById(friendId);
            return View("HeaderChatVC" , friend);
        }
    }
}
