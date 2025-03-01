using System.Security.Claims;
using Chat.Application.Services.ProfilePictureServices.Implementation;
using Chat.Application.Services.ProfilePictureServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.Interfaces;
using Chat.Domain.Models;
using Chat.Domain.DTOs;
using Chat.Domain.DTOs.AdminDTOs;
using Chat.Domain.Enum;

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

    public async Task<UserModel> GetByEmailOrUsernameAsync(string emailOrUsername)
    {
        return await userRepository.GetByEmailOrUsernameAsync(emailOrUsername);
    }

    public async Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username)
    {
        return await userRepository.ContainsUsernameAsync(username);
    }

    public async Task<bool> UserExistsAsync(string emailOrUsername, string password)
    {
        var user = await GetByEmailOrUsernameAsync(emailOrUsername);       
        if (user != null && user.Password == password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await userRepository.EmailExistsAsync(email);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await userRepository.UsernameExistsAsync(username);
    }

    public async Task<CreateUserResultEnum> CreateAsync(AdminCreateUserDTO user)
    {

        if (await UsernameExistsAsync(user.Username))
            return CreateUserResultEnum.UsernameAlreadyExists;

        if (await EmailExistsAsync(user.Email))
            return CreateUserResultEnum.EmailAlreadyExists;

        if (!await IsPasswordValid(user.Password))
            return CreateUserResultEnum.PasswordNotValid;


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
        return CreateUserResultEnum.Success;
    }

    public async Task<RegisterUserResultEnum> RegisterAsync(RegisterDTO user)
    {

        if (await UsernameExistsAsync(user.Username))
            return RegisterUserResultEnum.UsernameAlreadyExists;

        if (await EmailExistsAsync(user.Email))
            return RegisterUserResultEnum.EmailAlreadyExists;

        if (!await IsPasswordValid(user.Password))
            return RegisterUserResultEnum.PasswordNotValid;


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
        return RegisterUserResultEnum.Success;
    }

    public async Task<EditProfileDTO> GetForEditProfile(int id)
    {
        UserModel user = await GetByIdAsync(id);
        if (user == null)
            return null;

        var editUser = new EditProfileDTO()
        {
            UserId = user.UserId,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,           
        };
        return editUser;
    }

    public async Task<AdminEditUserDTO> GetForEditAdmin(int id)
    {
        UserModel user = await GetByIdAsync(id);
        if (user == null)
            return null;

        var editUser = new AdminEditUserDTO()
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

    public async Task Update(AdminEditUserDTO model)
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

    public async Task Update(EditProfileDTO model)
    {
        UserModel user = await GetByIdAsync(model.UserId);
        user.UserId = model.UserId;
        user.Name = model.Name;
        user.Username = model.Username;
        user.Email = model.Email;
        user.Password = model.Password;
        user.isAdmin = user.isAdmin;
        user.Picture = profilePicture.Edit(model, user);

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

    public async Task<bool> IsPasswordValid(string password)
    {
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await userRepository.SaveChangesAsync();
    }
}