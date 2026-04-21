using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC_WEB_APP.Models;
using MVC_WEB_APP.ViewModels;
using MVC_WEB_APP.ViewModels.UsersVm;
using Newtonsoft.Json;
using PustokMvcApp.Data;
using System;

namespace MVC_WEB_APP.Controllers
{
    public class AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        PustokMvcAppDbContext context

        ) : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await userManager.FindByNameAsync(vm.UsernameorEmail)
                       ?? await userManager.FindByEmailAsync(vm.UsernameorEmail);

            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or password incorrect");
                return View(vm);
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, vm.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Username/Email or password incorrect");
                return View(vm);
            }

            if (await userManager.IsInRoleAsync(user, "Admin") || await userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                ModelState.AddModelError("", "Admins cannot log in here.");
                return View(vm);
            }

            var result = await signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked out. Please try again later.");
                return View(vm);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or password incorrect");
                return View(vm);
            }

            MergeCookieBasketToUserBasket(user);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                ModelState.AddModelError("", "This username is already taken");
                return View(vm);
            }

            user = new AppUser
            {
                FullName = vm.FullName,
                UserName = vm.Username,
                Email = vm.Email
            };

            var result = await userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var exception in result.Errors)
                {
                    ModelState.AddModelError("", exception.Description);
                }

                return View(vm);
            }

            await signInManager.SignInAsync(user, false);
            await userManager.AddToRoleAsync(user, "User");

            MergeCookieBasketToUserBasket(user);

            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    return Content("Roles created successfully!");
        //}

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            Response.Cookies.Delete("basket");
            return RedirectToAction(nameof(Index), "Home");

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile(string tab = "dashboard")

        {
            ViewBag.Tab = tab;
            var user = await userManager.GetUserAsync(User);
            UserProfileVm vm = new UserProfileVm
            {
                UserInfo = new UserProfileInfoVm
                {
                    Username = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email
                }
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile(UserProfileVm vm)
        {
            ViewBag.Tab = "profile";
            if (!ModelState.IsValid) return View(vm);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Update basic user information
            user.FullName = vm.UserInfo.FullName;
            user.UserName = vm.UserInfo.Username;
            user.Email = vm.UserInfo.Email;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

            // Update password if fields are filled out
            if (!string.IsNullOrEmpty(vm.UserInfo.CurrentPassword) && !string.IsNullOrEmpty(vm.UserInfo.NewPassword))
            {
                var passwordResult = await userManager.ChangePasswordAsync(user, vm.UserInfo.CurrentPassword, vm.UserInfo.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError("UserInfo.NewPassword", error.Description);
                    }
                    return View(vm);
                }
            }

            // Refresh cookies to keep the user signed in with updated info
            await signInManager.RefreshSignInAsync(user);

            // Redirect on success to prevent form resubmission
            return RedirectToAction(nameof(UserProfile));
        }


        private void MergeCookieBasketToUserBasket(AppUser user)
        {
            var basket = Request.Cookies["basket"];

            if (string.IsNullOrEmpty(basket))
            {
                return;
            }

            var basketItems = JsonConvert.DeserializeObject<List<BasketItemVm>>(basket);

            if (basketItems == null || basketItems.Count == 0)
            {
                return;
            }

            foreach (var cookieItem in basketItems)
            {
                var existUserBasketItem = context.BasketItems
                    .FirstOrDefault(x => x.BookId == cookieItem.BookId && x.AppUserId == user.Id);

                if (existUserBasketItem == null)
                {
                    context.BasketItems.Add(new BasketItem
                    {
                        Id = Guid.NewGuid(),
                        BookId = cookieItem.BookId,
                        AppUserId = user.Id,
                        Count = cookieItem.Count
                    });
                }
                else
                {
                    existUserBasketItem.Count += cookieItem.Count;
                }
            }

            context.SaveChanges();

            Response.Cookies.Delete("basket");
        }
    }
}

