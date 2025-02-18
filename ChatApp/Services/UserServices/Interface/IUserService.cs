﻿using System.Security.Claims;
using ChatApp.ViewModels;
using DataLayer.Models;

namespace ChatApp.Services.UserServices.Interface;

public interface IUserService
{ 
    Task<IEnumerable<UserModel>> GetAllAsync();
    
    Task<UserModel> GetByIdAsync(int id);
    
    Task<UserModel> GetByEmailAsync(string email);
    
    Task<UserModel> GetByUsernameAsync(string username);
    
    Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
    
    Task<bool> UserExistsAsync(string email ,string password);
    
    Task CreateAsync(CreateUserViewModel user);
    
    Task<EditUserViewModel> GetForEdit(int id);
    
    Task Update(EditUserViewModel user);
    
    Task DeleteAsync(int id);
    
    ClaimsPrincipal PricipalUser(UserModel user);
    
    Task SaveChangesAsync();
}