using Chat.Domain.Models;
using Chat.Domain.ViewModels;

namespace Chat.Application.Services.ProfilePictureServices;

public static class ProfilePicure
    {
        
        #region Edit Photo
        public static string Edit(EditUserViewModel userViewModel, UserModel FoundUser)
        {
            if (userViewModel.ProfilePicture == null || userViewModel.ProfilePicture.FileName == FoundUser.Picture)
            {
                return FoundUser.Picture;
            }

            string fileName = Guid.NewGuid().ToString() + userViewModel.Username+
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
        public static string Add(CreateUserViewModel user)
        {
            string fileName = Guid.NewGuid().ToString() + user.Username+ Path.GetExtension(user.ProfilePicture.FileName);
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
        public static void Delete(UserModel model)
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