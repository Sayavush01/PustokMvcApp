using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_WEB_APP.Areas.Manage.ViewModels;
using MVC_WEB_APP.Models;

namespace MVC_WEB_APP.Areas.Manage.Controllers

{
    [Area("Manage")]
    public class AdminAccountController
        (UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager
        )
        : Controller
    {
        public async Task<IActionResult> CreateAdminAsync()
        {
            AppUser admin = new AppUser
            {
                UserName = "admin",
                FullName = "Admin User",
                Email = "admin@example.com"
            };
            IdentityResult result = await userManager.CreateAsync(admin, "_Admin123!");
            if (!result.Succeeded)
            {
                return Json(result.Errors);
            }

            await userManager.AddToRoleAsync(admin, "Admin");
            return Content("Admin user created successfully.");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(AdminLoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            AppUser admin = await userManager.FindByNameAsync(loginVm.Username);
            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginVm);
            }
            bool isPasswordValid = await userManager.CheckPasswordAsync(admin, loginVm.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginVm);
            }
            await signInManager.SignInAsync(admin, true);
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
           var user = await userManager.GetUserAsync(HttpContext.User);
           return Json(user);
        }
    }
}