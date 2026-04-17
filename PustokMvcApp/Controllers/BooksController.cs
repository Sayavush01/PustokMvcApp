using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.ViewModels;
using System.Security.Cryptography.Xml;

namespace PustokMvcApp.Controllers
{
    public class BooksController(PustokMvcAppDbContext context) : Controller
    {
        public IActionResult Detail(Guid id)
        {
            var book=context.Books
                .Include(x=>x.Author)
                .Include(x => x.BookImages)
                .Include(x => x.BookTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);
            BookDetailVm bookDetailVm= new()
            {
               Book =book,  
               RelatedBooks = context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.AuthorId==book.AuthorId&& x.Id!=book.Id)
                .Take(4)
                .ToList()

            };
            return View(bookDetailVm);
        }
        public IActionResult BookModal(Guid id)
        {
            var book=context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Include(x => x.BookTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();
            return PartialView("_BookModalPartial", book);

        }
    }
}
