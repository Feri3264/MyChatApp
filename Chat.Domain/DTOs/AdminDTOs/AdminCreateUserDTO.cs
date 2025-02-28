using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Chat.Domain.ViewModels.AdminViewModels;

public class AdminCreateUserDTO
{
    [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(200)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please Enter Your {0}"), MaxLength(100)]
    public string Password { get; set; }

    public bool isAdmin { get; set; }

    [Required(ErrorMessage = "Please Select Your {0}")]
    public IFormFile ProfilePicture { get; set; }
}
