﻿using Chat.Application.Services.FriendServices.Interface;
using Chat.Application.Services.ProfilePictureServices.Implementation;
using Chat.Application.Services.ProfilePictureServices.Interface;
using Chat.Application.Services.UserServices.Interface;
using Chat.Domain.DTOs.AdminDTOs;
using Chat.Domain.Enum;
using Chat.Domain.Models;
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
        public async Task<IActionResult> Index(int pageId = 1)
        {
            int take = 10;
            int pageCount = await UserService.GetCount() / take;
            int skip = (pageId - 1) * take;
            var userModel = await UserService.GetByTakeAsync(take , skip);

            ViewBag.CurrentPage = pageId;
            ViewBag.PageCount = pageCount;
            return View(userModel);
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
        public async Task<IActionResult> Create([Bind("Name,Username,Email,Password,isAdmin,ProfilePicture")] AdminCreateUserDTO userModel)
        {                 
            if (ModelState.IsValid)
            {
                var result = await UserService.CreateAsync(userModel);
                switch (result)
                {
                    case CreateUserResultEnum.Success:
                        await UserService.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    case CreateUserResultEnum.EmailAlreadyExists:
                        ModelState.AddModelError("Email", "Email Alreay Exists");
                        break;

                    case CreateUserResultEnum.UsernameAlreadyExists:
                        ModelState.AddModelError("Username", "Username Alreay Exists");
                        break;

                    case CreateUserResultEnum.PasswordNotValid:
                        ModelState.AddModelError("Password", "Password Must Contains 8 Characters");
                        break;
                }                
                
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
            
            var userModel = await UserService.GetForEditUser((int)id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserId, [Bind("UserId,Name,Username,Email,isAdmin,ProfilePicture")] AdminEditUserDTO userModel)
        {
            if (UserId != userModel.UserId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var result = await UserService.Update(userModel);
                switch (result)
                {
                    case EditUserResultEnum.Success:
                        await UserService.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    case EditUserResultEnum.EmailAlreadyExists:
                        ModelState.AddModelError("Email", "Email Alreay Exists");
                        break;

                    case EditUserResultEnum.UsernameAlreadyExists:
                        ModelState.AddModelError("Username", "Username Alreay Exists");
                        break;
                }               
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
            
            await UserService.DeleteAsync(UserId);
            await UserService.SaveChangesAsync();
            profilePicture.Delete(userModel);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}    