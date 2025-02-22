using Chat.Application.Services.MessageServices.Interface;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Components
{
    public class MessageViewComponent 
        (IMessageService MessageService) : ViewComponent
    {
        
        // === Getting Data --> ReturnMessageViewComponent Action (ChatController) --> Ajax (Chat View) ===
        public async Task<IViewComponentResult> InvokeAsync(FriendModel friendship)
        {
            var messages = await MessageService.GetByFriendshipAsync(friendship);  
            if(messages == null)
                return Content("NotFound");
            
            var result = messages.OrderBy(m => m.MessageDate).ToList();
            ViewData["MessageUserId"]= friendship.UserId; 
            return View("MessageVC", result);                       
        }
    }
}