using System.Security.Claims;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Chat.Domain.DTOs.AdminDTOs;
using Chat.Domain.Enum;

namespace Chat.Application.Services.UserServices.Interface;

public interface IUserService
{
   
    #region GetBy
    Task<IEnumerable<UserModel>> GetAllAsync();

    Task<UserModel> GetByIdAsync(int id);

    Task<UserModel> GetByEmailOrUsernameAsync(string emailOrUsername);

    Task<List<UserModel>> GetByTakeAsync(int take, int skip);
    #endregion

    #region Exists
    Task<bool> UserExistsAsync(string emailOrUsername, string password);

    Task<bool> EmailExistsAsync(string email);

    Task<bool> UsernameExistsAsync(string username);

    Task<bool> EditEmailExistsAsync(string email, int usersId);

    Task<bool> EditUsernameExistsAsync(string username, int usersId);
    #endregion

    #region Create
    Task<CreateUserResultEnum> CreateAsync(AdminCreateUserDTO user);

    Task<RegisterUserResultEnum> RegisterAsync(RegisterDTO user);
    #endregion

    #region Edit
    Task<EditProfileDTO> GetForEditProfile(int id);

    Task<AdminEditUserDTO> GetForEditUser(int id);

    Task<EditUserResultEnum> Update(AdminEditUserDTO user);

    Task Update(EditProfileDTO user);
    #endregion

    #region Authentication
    ClaimsPrincipal PricipalUser(UserModel user);
    #endregion

    #region Delete
    Task DeleteAsync(int id);
    #endregion

    #region Tools
    Task<int> GetCount();

    Task<ChangePasswordResultEnum> ChangePassword(int userId, string password);

    Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
    #endregion

    #region Save
    Task SaveChangesAsync();
    #endregion
}