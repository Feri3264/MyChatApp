using System.Security.Claims;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Chat.Domain.DTOs.AdminDTOs;

namespace Chat.Application.Services.UserServices.Interface;

public interface IUserService
{ 
    Task<IEnumerable<UserModel>> GetAllAsync();
    
    Task<UserModel> GetByIdAsync(int id);
    
    Task<UserModel> GetByEmailAsync(string email);
    
    Task<UserModel> GetByUsernameAsync(string username);
    
    Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
    
    Task<bool> UserExistsAsync(string email ,string password);
    
    Task CreateAsync(AdminCreateUserDTO user);

    Task RegisterAsync(RegisterDTO user);
    
    Task<EditProfileDTO> GetForEditProfile(int id);

    Task<AdminEditUserDTO> GetForEditAdmin(int id);
    
    Task Update(AdminEditUserDTO user);

    Task Update(EditProfileDTO user);
    
    Task DeleteAsync(int id);
    
    ClaimsPrincipal PricipalUser(UserModel user);
    
    Task SaveChangesAsync();
}