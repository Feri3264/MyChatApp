using ChatApp.ViewModels;
using DataLayer.Models;

namespace ChatApp.Services;

public static class ProfilePicure
    {
        
        #region Edit Photo
        public static string Edit(EditUserViewModel userViewModel, UserModel FoundUser)
        {
            if (userViewModel.ProfilePicture == null || userViewModel.ProfilePicture.FileName == FoundUser.Picture)
            {
                return FoundUser.Picture;
            }

            string fileName = Guid.NewGuid().ToString() + userViewModel.Username.ToString() +
                              Path.GetExtension(userViewModel.ProfilePicture.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "ProfilePicture");
            string NewFilePath = Path.Combine(path, fileName);
            using (var stream = new FileStream(NewFilePath, FileMode.Create))
            {
                userViewModel.ProfilePicture.CopyTo(stream);
            }

            string oldFilePath = Path.Combine(path, FoundUser.Picture);
            System.IO.File.Delete(oldFilePath);
            return fileName;
        }
        #endregion

        #region Add Photo
        public static string Add(CreateUserViewModel user)
        {
            string fileName = Guid.NewGuid().ToString() + user.Username.ToString() + Path.GetExtension(user.ProfilePicture.FileName);
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

    }