using Microsoft.Extensions.DependencyInjection;
using Chat.Domain.Interfaces;
using Chat.Data.Repository;
using Chat.Application.Services.UserServices.Interface;
using Chat.Application.Services.UserServices.Implementation;
using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.FriendServices.Implementation;
using Chat.Application.Services.MessageServices.Interface;
using Chat.Application.Services.MessageServices.Implementation;
using Chat.Data.Context;

namespace Chat.IOC;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ChatContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFriendRepository, FriendRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFriendService, FriendService>();
        services.AddScoped<IMessageService, MessageService>();
    }
}