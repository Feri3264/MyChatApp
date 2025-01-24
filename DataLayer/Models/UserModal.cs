﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Models
{
    public class UserModal
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

        public string? Picture { get; set; }



        //Navigation Properties
        public List<FriendModal> Friends { get; set; }
        public List<MessageModal> Messages { get; set; }
    }
}