using System.Security.Claims;
using Chat.Domain.Models;
using Chat.Domain.ViewModels;
using Chat.Domain.ViewModels.AdminViewModels;

namespace Chat.Application.Services.UserServices.Interface;

public interface IUserService
{ 
    Task<IEnumerable<UserModel>> GetAllAsync();
    
    Task<UserModel> GetByIdAsync(int id);
    
    Task<UserModel> GetByEmailAsync(string email);
    
    Task<UserModel> GetByUsernameAsync(string username);
    
    Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
    
    Task<bool> UserExistsAsync(string email ,string password);
    
    Task CreateAsync(AdminCreateUserViewModel user);

    Task RegisterAsync(RegisterViewModel user);
    
    Task<EditProfileViewModel> GetForEditProfile(int id);

    Task<AdminEditUserViewModel> GetForEditAdmin(int id);
    
    Task Update(AdminEditUserViewModel user);

    Task Update(EditProfileViewModel user);
    
    Task DeleteAsync(int id);
    
    ClaimsPrincipal PricipalUser(UserModel user);
    
    Task SaveChangesAsync();
}