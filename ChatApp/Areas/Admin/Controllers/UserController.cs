using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.ViewModels;
using ChatApp.Services.UserServices.Interface;

namespace ChatApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("/Admin/User/{action=index}")]
    public class UserController 
        (IUserService UserService) : Controller
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
        public async Task<IActionResult> Create([Bind("UserId,Name,Username,Email,ConfirmPassword,Password,isAdmin,ProfilePicture")] CreateUserViewModel userModel)
        {                 
            if (ModelState.IsValid)
            {
                await UserService.CreateAsync(userModel);
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
            
            var userModel = await UserService.GetForEdit((int)id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Admin/User/Edit/5
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
            var userModel = UserService.DeleteAsync(UserId);
            if (userModel == null)
            {
                return NotFound();
            }

            await UserService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
