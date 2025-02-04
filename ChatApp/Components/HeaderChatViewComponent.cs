using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class HeaderChatViewComponent : ViewComponent
    {

        IUserRepository _userRepository;
        public HeaderChatViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public IViewComponentResult Invoke(int friendId)
        {
            UserModel friend = _userRepository.FindUserById(friendId);
            return View("HeaderChatVC" , friend);
        }
    }
}
