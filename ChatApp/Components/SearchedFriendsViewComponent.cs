using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class SearchedFriendsViewComponent : ViewComponent
    {

        ChatContext _context;
        public SearchedFriendsViewComponent(ChatContext context)
        {
                _context = context;
        }
       
        public IViewComponentResult Invoke(string username)
        {
            List<UserModel> friends = _context.Users
                .Where(x => x.Username.Contains(username))
                .Select(x => x)
                .ToList();
            return View("SearchedFriendsVC" , friends);
        }

    }
}
