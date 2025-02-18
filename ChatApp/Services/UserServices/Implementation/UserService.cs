﻿using System.Security.Claims;
using ChatApp.Services.UserServices.Interface;
using ChatApp.ViewModels;
using DataLayer.Models;
using DataLayer.Repository;


namespace ChatApp.Services.UserServices.Implementation;

public class UserService
    (IUserRepository userRepository)  : IUserService
{
    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        return await userRepository.GetByIdAsync(id);
    }

    public async Task<UserModel> GetByEmailAsync(string email)
    {
        return await userRepository.GetByEmailAsync(email);
    }

    public async Task<UserModel> GetByUsernameAsync(string username)
    {
        return await userRepository.GetByUsernameAsync(username);
    }

    public async Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username)
    {
        return await userRepository.ContainsUsernameAsync(username);
    }

    public async Task<bool> UserExistsAsync(string email, string password)
    {
        UserModel user = await GetByEmailAsync(email);
        if (user.Password == password)
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
            Picture = ProfilePicure.Add(user)
        };
        
        await userRepository.AddAsync(newUser);
    }

    public async Task<EditUserViewModel> GetForEdit(int id)
    {
        UserModel user = await GetByIdAsync(id);
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
        user.Picture = ProfilePicure.Edit(model , user);
        
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