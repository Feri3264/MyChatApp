using System.Security.Claims;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Chat.Domain.DTOs.AdminDTOs;
using Chat.Domain.Enum;

namespace Chat.Application.Services.UserServices.Interface;

public interface IUserService
{ 
    Task<IEnumerable<UserModel>> GetAllAsync();
    
    Task<UserModel> GetByIdAsync(int id);

    Task<UserModel> GetByEmailOrUsernameAsync(string emailOrUsername); 
    
    Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
    
    Task<bool> UserExistsAsync(string emailOrUsername ,string password);

    Task<bool> EmailExistsAsync(string email);

    Task<bool> UsernameExistsAsync(string username);

    Task<bool> EditEmailExistsAsync(string email, int usersId);

    Task<bool> EditUsernameExistsAsync(string username, int usersId);

    Task<CreateUserResultEnum> CreateAsync(AdminCreateUserDTO user);

    Task<RegisterUserResultEnum> RegisterAsync(RegisterDTO user);
    
    Task<EditProfileDTO> GetForEditProfile(int id);

    Task<AdminEditUserDTO> GetForEditUser(int id);

    Task<EditUserResultEnum> Update(AdminEditUserDTO user);

    Task Update(EditProfileDTO user);
    
    Task DeleteAsync(int id);
    
    ClaimsPrincipal PricipalUser(UserModel user);
    
    Task SaveChangesAsync();
}