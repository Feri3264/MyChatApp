using ChatApp.Services.UserServices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class SearchedFriendsViewComponent 
        (IUserService UserService) : ViewComponent
    {
       
       // === Getting Data --> ReturnSearchedFriendsViewComponent Action (SearchController) --> Ajax (Search View)
        public async Task<IViewComponentResult> Invoke(string username)
        {
            var friends = await UserService.ContainsUsernameAsync(username);
            return View("SearchedFriendsVC" , friends);
        }

    }
}
