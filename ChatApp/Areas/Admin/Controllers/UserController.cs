using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using ChatApp.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using DataLayer.Repository;
using ChatApp.Services;

namespace ChatApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("/Admin/User/{action=index}")]
    public class UserController 
        (IUserRepository _userRepository) : Controller
    {

        #region Index
        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAllUsers());
        }
        #endregion

        #region Details
        // GET: Admin/User/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _userRepository.FindUserById(id);
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
        public IActionResult Create([Bind("UserId,Name,Username,Email,ConfirmPassword,Password,isAdmin,ProfilePicture")] CreateUserViewModel userModel)
        {                 
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel
                {
                    Name = userModel.Name,
                    Username = userModel.Username,
                    Email = userModel.Email,
                    Password = userModel.Password,
                    isAdmin = userModel.isAdmin,
                    Picture = ProfilePicure.Add(userModel)
                };
                _userRepository.AddUser(user);
                _userRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }
        #endregion

        #region Edit
        // GET: Admin/User/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var userModel = _userRepository.FindUserById(id);
            if (userModel == null)
            {
                return NotFound();
            }
            
            var user = new EditUserViewModel()
            {
                UserId = userModel.UserId,
                Name = userModel.Name,
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password,
                isAdmin = userModel.isAdmin,
            };
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int UserId, [Bind("UserId,Name,Username,Email,Password,isAdmin,ProfilePicture")] EditUserViewModel userModel)
        {
            if (UserId != userModel.UserId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                UserModel user = _userRepository.FindUserById(UserId);
                user.UserId = userModel.UserId;
                user.Name = userModel.Name;
                user.Username = userModel.Username;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.isAdmin = userModel.isAdmin;
                user.Picture = ProfilePicure.Edit(userModel , user);
                
                _userRepository.UpdateUser(user);
                _userRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }
        #endregion

        #region Delete
        // GET: Admin/User/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _userRepository.FindUserById(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int UserId)
        {
            var userModel = _userRepository.FindUserById(UserId);
            if (userModel != null)
            {
                _userRepository.RemoveUser(userModel);
            }

            _userRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Tools
        private bool UserModelExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        #endregion


    }
}
