using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.ProfilePictureServices.Implementation;
using Chat.Application.Services.ProfilePictureServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.DTOs.AdminDTOs;
using Chat.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("/Admin/User/{action=index}")]
    public class UserController 
        (IUserService UserService , IFriendService FriendService , IProfilePicture profilePicture) : Controller
    {

        #region Index
        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            return View(await UserService.GetAllAsync());
        }
        #endregion

        #region Details
        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel =await UserService.GetByIdAsync(((int)id));
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }
        #endregion

        #region Create
        // GET: Admin/User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Username,Email,Password,isAdmin,ProfilePicture")] AdminCreateUserDTO userModel)
        {                 
            if (ModelState.IsValid)
            {
                var result = await UserService.CreateAsync(userModel);
                switch (result)
                {
                    case CreateUserResultEnum.Success:
                        await UserService.SaveChangesAsync();
                        return RedirectToAction("Login");

                    case CreateUserResultEnum.UserAlreadyExists:
                        ModelState.AddModelError("", "User Alreay Exists");
                        break;

                    case CreateUserResultEnum.EmailAlreadyExists:
                        ModelState.AddModelError("Email", "Email Alreay Exists");
                        break;

                    case CreateUserResultEnum.UsernameAlreadyExists:
                        ModelState.AddModelError("Username", "Username Alreay Exists");
                        break;

                    case CreateUserResultEnum.PasswordNotValid:
                        ModelState.AddModelError("Password", "Password Not Valid");
                        break;
                }
                await UserService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }
        #endregion

        #region Edit
        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var userModel = await UserService.GetForEditAdmin((int)id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserId, [Bind("UserId,Name,Username,Email,Password,isAdmin,ProfilePicture")] AdminEditUserDTO userModel)
        {
            if (UserId != userModel.UserId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                await UserService.Update(userModel);
                await UserService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }
        #endregion

        #region Delete
        // GET: Admin/User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await UserService.GetByIdAsync((int)id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserId)
        {
            var userModel = await UserService.GetByIdAsync(UserId);
            await FriendService.DeleteAllFriendsAsync(userModel.Friends);
            
            await UserService.DeleteAsync(UserId);
            await UserService.SaveChangesAsync();
            profilePicture.Delete(userModel);
            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
