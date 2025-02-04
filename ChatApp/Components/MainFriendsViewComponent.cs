using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MainFriendsViewComponent : ViewComponent
    {       
        IUserRepository _userRepository;
        public MainFriendsViewComponent(IUserRepository userRepository)
        {            
            _userRepository = userRepository;
        }


        public IViewComponentResult Invoke(UserModel model)
        {
            ViewData["UserId"] = model.UserId;
            List<UserModel> users = new List<UserModel>();
            
            foreach (var item in model.Friends)
            {
                var result = _userRepository.FindUserById(item.FreindId);
                users.Add(result);
            }

            return View("MainFriendsVC" , users);
        }
    }
}
