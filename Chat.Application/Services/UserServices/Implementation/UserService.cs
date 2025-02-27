using System.Security.Claims;
using Chat.Application.Services.ProfilePictureServices.Implementation;
using Chat.Application.Services.ProfilePictureServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;
using Chat.Domain.ViewModels;

namespace Chat.Application.Services.UserServices.Implementation;

public class UserService
    (IUserRepository userRepository , IProfilePicture profilePicture)  : IUserService
{
    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return null;
        
        return user;
    }

    public async Task<UserModel> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);
        if (user == null)
            return null;
        
        return user;
    }

    public async Task<UserModel> GetByUsernameAsync(string username)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null)
            return null;

        return user;
    }

    public async Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username)
    {
        return await userRepository.ContainsUsernameAsync(username);
    }

    public async Task<bool> UserExistsAsync(string email, string password)
    {
        UserModel user = await GetByEmailAsync(email);
        if (user != null && user.Password == password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task CreateAsync(CreateUserViewModel user)
    {
        UserModel newUser = new UserModel
        {
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            isAdmin = user.isAdmin,
            Picture = profilePicture.Add(user)
        };
        
        await userRepository.AddAsync(newUser);
    }

    public async Task RegisterAsync(RegisterViewModel user)
    {
        UserModel newUser = new UserModel
        {
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            isAdmin = false,
            Picture = profilePicture.Add(user)
        };

        await userRepository.AddAsync(newUser);
    }

    public async Task<EditUserViewModel> GetForEdit(int id)
    {
        UserModel user = await GetByIdAsync(id);
        if (user == null)
            return null;
        
        var editUser = new EditUserViewModel()
        {
            UserId = user.UserId,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            isAdmin = user.isAdmin,
        };
        return editUser;
    }

    public async Task Update(EditUserViewModel model)
    {
        UserModel user = await GetByIdAsync(model.UserId);
        user.UserId = model.UserId;
        user.Name = model.Name;
        user.Username = model.Username;
        user.Email = model.Email;
        user.Password = model.Password;
        user.isAdmin = model.isAdmin;
        user.Picture = profilePicture.Edit(model , user);
        
        userRepository.Update(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        await userRepository.DeleteAsync(user);
    }

    public ClaimsPrincipal PricipalUser(UserModel user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim (ClaimTypes.NameIdentifier , user.UserId.ToString() , ClaimValueTypes.Integer32),
            new Claim (ClaimTypes.Name , user.Username, ClaimValueTypes.String),
            new Claim (ClaimTypes.GivenName , user.Name , ClaimValueTypes.String),
            new Claim ("Admin" , user.isAdmin.ToString() , ClaimValueTypes.Boolean)
        };

        var identity = new ClaimsIdentity(claims, "login");
        var principal = new ClaimsPrincipal(identity);
        return principal;
    }

    public async Task SaveChangesAsync()
    {
        await userRepository.SaveChangesAsync();
    }
}