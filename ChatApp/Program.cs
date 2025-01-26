using DataLayer.Context;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//#region DI
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IFriendRepository, FriendRepository>();
//builder.Services.AddScoped<IMessageRepository, MessageRepository>();
//#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}")
    .WithStaticAssets();


app.Run();
