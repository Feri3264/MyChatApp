using System.ComponentModel.DataAnnotations;

namespace ChatApp.ViewModels
{
    public class RegisterViewModel
    {        

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Select Your {0}")]
        public IFormFile ProfilePicture { get; set; }
    }
}
