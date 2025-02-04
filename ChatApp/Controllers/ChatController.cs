using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {

        IFriendRepository _friendRepository;
        IMessageRepository _messageRepository;
        public ChatController(IFriendRepository friendRepository, IMessageRepository messageRepository)
        {
            _friendRepository = friendRepository;
            _messageRepository = messageRepository;
        }


        [HttpGet("/chat/{userId}/{friendId}")]
        public IActionResult Chat(int userId, int friendId)
        {
            FriendModel friendship = _friendRepository.FindFriendship(friendId, userId);
            return View(friendship);
        }


        [HttpPost]
        public IActionResult ReturnMessageViewComponent(FriendModel data)
        {            
            return ViewComponent("Message" , data);
        }


        [HttpPost]
        public string AddMessageAction(MessageModel data)
        { 
            _messageRepository.AddMessage(data); 
            _messageRepository.SaveChanges();
            return "";
        }
    }
}
