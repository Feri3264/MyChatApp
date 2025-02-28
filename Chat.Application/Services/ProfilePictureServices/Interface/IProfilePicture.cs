using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using Chat.Domain.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.ProfilePictureServices.Interface
{
    public interface IProfilePicture
    {
        string Edit(AdminEditUserViewModel userViewModel, UserModel FoundUser);
        string Edit(EditProfileViewModel userViewModel, UserModel FoundUser);
        string Add(AdminCreateUserViewModel user);
        string Add(RegisterViewModel user);
        void Delete(UserModel model);
    }
}
