using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVC_WEB_APP.Models;

namespace MVC_WEB_APP.Controllers
{
    public class AuthorController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager

        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    return Content("Roles created successfully!");
        //}
    }
}
