﻿using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}") , MaxLength(100)]        
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}") , MaxLength(200)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}") , MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
        public string Password { get; set; }

        public bool isAdmin { get; set; }

        [Required(ErrorMessage = "Please Select Your {0}")]
        public string Picture { get; set; }



        //Navigation Properties
        public List<FriendModel>? Friends { get; set; }
    }
}