using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_WEB_APP.Models;

namespace MVC_WEB_APP.Areas.Manage.Controllers
{
    public class AdminAccountController 
        (UserManager<AppUser> userManager)
        : Controller
    {
        public async Task<IActionResult> CreateAdminAsync()
        {
            AppUser admin= new AppUser
                            {
                UserName = "admin",
                FullName = "Admin User",
                Email = "admin@example.com"
            };
            IdentityResult result = await userManager.CreateAsync(admin, "Admin123");
            if (!result.Succeeded)
            {
                return Content("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return View(); 
        }
    }
}
