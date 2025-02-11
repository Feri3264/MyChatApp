using System.ComponentModel.DataAnnotations;

namespace ChatApp.Areas.Admin.ViewModels
{
    public class EditUserViewModel
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

        public bool isAdmin { get; set; }

        public IFormFile? ProfilePicture { get; set; }
    }
}
