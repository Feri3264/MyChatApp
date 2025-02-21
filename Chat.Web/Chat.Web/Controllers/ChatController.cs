using System.Security.Claims;
using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.MessageServices.Interface;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class ChatController 
        (IFriendService FriendService, IMessageService MessageService) : Controller
    {

        // === Getting Data From User Page ===
        [HttpGet("/chat/{userId}/{friendId}")]
        public async Task<IActionResult> Chat(int userId, int friendId)
        {
            FriendModel friendship = await FriendService.GetByIdAsync(userId, friendId);
            if (friendship == null)
            {
                return NotFound();
            }
            return View(friendship);
        }


        // === Getting Data From Ajax in Chat cshtml ===
        // === Adding Messages ===
        [HttpPost]
        public async Task<string> AddMessageAction(MessageModel data)
        {
            if (data.MessageText == null || data.MessageText == " ")
            {
                return "";
            }
            await MessageService.CreateAsync(data);
            await MessageService.SaveChangesAsync();
            return "";
        }

        
        public async Task DeleteMessage(int MessageId , int sender)
        {
            var claims = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier);
            if (claims.Value == sender.ToString())
            {
                await MessageService.DeleteAsync(MessageId);
                await MessageService.SaveChangesAsync();
            }
            else
            {
                NotFound();
            }
        }


        // === Getting Data From Ajax in Chat cshtml === 
        // === Fetching Messages ===
        [HttpPost]
        public IActionResult ReturnMessageViewComponent(FriendModel data)
        {            
            return ViewComponent("Message" , data);
        }
    }
}
