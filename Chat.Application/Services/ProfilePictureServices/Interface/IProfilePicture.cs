using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Chat.Domain.DTOs.AdminDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.ProfilePictureServices.Interface
{
    public interface IProfilePicture
    {
        string Edit(AdminEditUserDTO userViewModel, UserModel FoundUser);
        string Edit(EditProfileDTO userViewModel, UserModel FoundUser);
        string Add(AdminCreateUserDTO user);
        string Add(RegisterDTO user);
        void Delete(UserModel model);
    }
}
