using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class SearchController 
        (IFriendRepository _friendRepository, IUserRepository _userRepository) : Controller
    {



        [HttpGet("/search/{userId}")]
        public IActionResult Search(int userId)
        {            
            TempData["userId"] = userId;
            return View();
        }


        [HttpGet("/AddFriend")]
        public IActionResult AddFriend(int friendId)
        {
            int userId = (int)TempData["userId"];
            FriendModel friendship = new FriendModel
            {
                UserId = userId,
                FreindId = friendId
            };


            if(friendship.UserId == null || friendship.FreindId == null)
                return NotFound();


            _friendRepository.AddFriend(friendship);
            _friendRepository.SaveChanges();

            UserModel user =  _userRepository.FindUserById(userId);
            return Redirect($"/{user.Username}");
        }


        [HttpPost]
        public IActionResult ReturnSearchedFriendsViewComponent(string data)
        {
            return ViewComponent("SearchedFriends", data);
        }
    }
}