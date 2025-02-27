using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.ProfilePictureServices.Interface
{
    public interface IProfilePicture
    {
        string Edit(EditUserViewModel userViewModel, UserModel FoundUser);
        string Add(CreateUserViewModel user);
        string Add(RegisterViewModel user);
        void Delete(UserModel model);
    }
}
