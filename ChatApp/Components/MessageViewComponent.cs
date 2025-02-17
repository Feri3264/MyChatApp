using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Components
{
    public class MessageViewComponent 
        (IMessageRepository _messageRepository) : ViewComponent
    {
        
        // === Getting Data --> ReturnMessageViewComponent Action (ChatController) --> Ajax (Chat View) ===
        public async Task<IViewComponentResult> InvokeAsync(FriendModel friendship)
        {
            var messages = await _messageRepository.FindMessagesByFriendship(friendship);            
            var result = messages.OrderBy(m => m.MessageDate).ToList();
            ViewData["MessageUserId"]= friendship.UserId; 
            return View("MessageVC", result);                       
        }
    }
}