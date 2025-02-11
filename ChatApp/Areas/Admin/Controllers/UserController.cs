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
using ChatApp.Areas.Admin.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ChatApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("/Admin/User/{action=index}")]
    public class UserController 
        (ChatContext _context) : Controller
    {

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }


        #region Details
        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Username,Email,Password,isAdmin,Picture")] UserModel userModel)
        {
            userModel.Friends = null;
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
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


            var userModel = await _context.Users.FindAsync(id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserId, [Bind("UserId,Name,Username,Email,Password,isAdmin,ProfilePicture")] EditUserViewModel userModel)
        {
            if (UserId != userModel.UserId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                UserModel user = _context.Users.Find(UserId);
                user.UserId = userModel.UserId;
                user.Name = userModel.Name;
                user.Username = userModel.Username;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.isAdmin = userModel.isAdmin;
                user.Picture = EditProfilePic(userModel, user);
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
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
            var userModel = await _context.Users.FindAsync(UserId);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Tools
        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }


        public string EditProfilePic(EditUserViewModel userViewModel, UserModel FoundUser)
        {
            if (userViewModel.ProfilePicture == null || userViewModel.ProfilePicture.FileName == FoundUser.Picture)
            {
                return FoundUser.Picture;
            }

            string fileName = Guid.NewGuid().ToString() + userViewModel.Username.ToString() + Path.GetExtension(userViewModel.ProfilePicture.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "ProfilePicture");
            string NewFilePath = Path.Combine(path, fileName);
            using (var stream = new FileStream(NewFilePath, FileMode.Create))
            {
                userViewModel.ProfilePicture.CopyTo(stream);
            }
            string oldFilePath = Path.Combine(path, FoundUser.Picture);
            System.IO.File.Delete(oldFilePath);
            return fileName;
        }
        #endregion


    }
}
