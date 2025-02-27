using Chat.Application.Services.ProfilePictureServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using System.Threading.Tasks;

namespace Chat.Application.Services.ProfilePictureServices.Implementation;

public class ProfilePicure
    (IUserService userService) : IProfilePicture
{
    #region Edit Photo
    public string Edit(EditUserViewModel userViewModel, UserModel FoundUser)
    {
        if (userViewModel.ProfilePicture == null || userViewModel.ProfilePicture.FileName == FoundUser.Picture)
        {
            return FoundUser.Picture;
        }

        string fileName = Guid.NewGuid().ToString() + userViewModel.Username +
                          Path.GetExtension(userViewModel.ProfilePicture.FileName);
        string path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            "ProfilePicture");
        string newFilePath = Path.Combine(path, fileName);
        using (var stream = new FileStream(newFilePath, FileMode.Create))
        {
            userViewModel.ProfilePicture.CopyTo(stream);
        }

        Delete(FoundUser);
        return fileName;
    }
    #endregion

    #region Add Photo
    public string Add(CreateUserViewModel user)
    {
        string fileName = Guid.NewGuid().ToString() + user.Username + Path.GetExtension(user.ProfilePicture.FileName);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            "ProfilePicture",
            fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            user.ProfilePicture.CopyTo(stream);
        }
        return fileName;
    }

    public string Add(RegisterViewModel user)
    {
        string fileName = Guid.NewGuid().ToString() + user.Username + Path.GetExtension(user.ProfilePicture.FileName);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            "ProfilePicture",
            fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            user.ProfilePicture.CopyTo(stream);
        }
        return fileName;
    }
    #endregion

    #region Delete
    public void Delete(UserModel model)
    {
        string fileName = model.Picture;
        string path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            "ProfilePicture");
        string filePath = Path.Combine(path, fileName);
        File.Delete(filePath);
    }
    #endregion

}