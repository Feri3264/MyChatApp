using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.ViewModels
{
    public class EditProfileDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
        public string Password { get; set; }      

        public IFormFile? ProfilePicture { get; set; }
    }
}
