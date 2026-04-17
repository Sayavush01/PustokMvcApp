using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.Models;
using PustokMvcApp.ViewModels;
using System.Diagnostics;

namespace PustokMvcApp.Controllers
{
    public class HomeController(PustokMvcAppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = context.Sliders.ToList(),
                FeaturedBooks = context.Books
                .Include(x => x.Author)
                .Include(x=>x.BookImages)
                .Where(x => x.IsFeatured)
                .ToList(),


                NewBooks = context.Books
                .Include(p => p.Author)
                .Include(p=>p.BookImages)
                .Where(x => x.IsNew)
                .ToList(),



                DiscountedBooks = context.Books
                .Include(p => p.Author)
                .Include(p => p.BookImages)
                .Where(x => x.DiscountPercentage > 0)
                .ToList()
            };
            return View(homeVm);
        }

        
    }
}
