﻿using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController 
        (IFriendRepository _friendRepository, IMessageRepository _messageRepository) : Controller
    {

        // === Getting Data From User Page ===
        [HttpGet("/chat/{userId}/{friendId}")]
        public IActionResult Chat(int userId, int friendId)
        {
            FriendModel friendship = _friendRepository.FindFriendship(friendId, userId);
            if (friendship == null)
            {
                return NotFound();
            }
            return View(friendship);
        }


        // === Getting Data From Ajax in Chat cshtml ===
        // === Adding Messages ===
        [HttpPost]
        public string AddMessageAction(MessageModel data)
        {
            if (data.MessageText == null || data.MessageText == " ")
            {
                return "";
            }
            _messageRepository.AddMessage(data);
            _messageRepository.SaveChanges();
            return "";
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
