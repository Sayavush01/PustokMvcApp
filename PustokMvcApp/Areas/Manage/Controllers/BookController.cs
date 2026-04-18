using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.Extensions;
using PustokMvcApp.Models;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly PustokMvcAppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(PustokMvcAppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
                
            return View(books);
        }

        public async Task<IActionResult> GenerateFake(int count = 5)
        {
            var random = new Random();
            var authors = await _context.Authors.ToListAsync();

            // Ensure we have at least one author in the DB to relate to
            if (!authors.Any())
            {
                var defaultAuthor = new Models.Author { FullName = "Dostoyevski" };
                _context.Authors.Add(defaultAuthor);
                await _context.SaveChangesAsync();
                authors.Add(defaultAuthor);
            }

            var images = new[] { "product-1.jpg", "product-2.jpg", "product-3.jpg", "product-4.jpg", "product-5.jpg" };

            for (int i = 0; i < count; i++)
            {
                var author = authors[random.Next(authors.Count)];
                
                int generatedCode = random.Next(1, 9999);

                _context.Books.Add(new Models.Book
                {
                    Name = $"Book{generatedCode}",
                    Description = "This is a beautiful auto-generated book description.",
                    Price = random.Next(10, 50),
                    DiscountPercentage = random.Next(0, 20),
                    InStock = random.Next(0, 2) == 1,
                    MainImageUrl = images[random.Next(images.Length)],
                    HoverImageUrl = images[random.Next(images.Length)],
                    IsNew = true,
                    IsFeatured = true,
                    Code = generatedCode,
                    AuthorId = author.Id
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var authors = _context.Authors.ToList();
            var tags = _context.Tags.ToList();

            // Seed an author if database is completely empty
            if (!authors.Any())
            {
                var defaultAuthor = new Models.Author { FullName = "Dostoyevski" };
                _context.Authors.Add(defaultAuthor);
                _context.SaveChanges();
                authors.Add(defaultAuthor);
            }

            // Seed some dummy tags if database is completely empty
            if (!tags.Any())
            {
                var defaultTags = new List<Models.Tag>
                {
                    new Models.Tag { Name = "New" },
                    new Models.Tag { Name = "Bestseller" },
                    new Models.Tag { Name = "Fiction" },
                    new Models.Tag { Name = "Classic" }
                };
                _context.Tags.AddRange(defaultTags);
                _context.SaveChanges();
                tags.AddRange(defaultTags);
            }

            ViewBag.Authors = authors;
            ViewBag.Tags = tags;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (book.MainPhoto == null) ModelState.AddModelError("MainPhoto", "Main photo must be selected!");
            if (book.HoverPhoto == null) ModelState.AddModelError("HoverPhoto", "Hover photo must be selected!");

            // Prevent strict ModelState errors on navigational properties or dynamically set fields
            ModelState.Remove("Author");
            ModelState.Remove("BookImages");
            ModelState.Remove("BookTags");
            ModelState.Remove("MainImageUrl");
            ModelState.Remove("HoverImageUrl");

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _context.Authors.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View(book);
            }

            if (!_context.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found");
                ViewBag.Authors = await _context.Authors.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View(book);
            }

            book.MainImageUrl = book.MainPhoto.SaveFile("assets\\image\\products", _env.WebRootPath);
            book.HoverImageUrl = book.HoverPhoto.SaveFile("assets\\image\\products", _env.WebRootPath);

            var bookImages = new List<BookImage>();
            if (book.Files != null)
            {
                foreach (var file in book.Files)
                {
                    bookImages.Add(new BookImage
                    {
                        Image = file.SaveFile("assets\\image\\products", _env.WebRootPath)
                        // Removed `Book = book` because EF Core will safely assign the FK natively.
                    });
                }
            }
            book.BookImages = bookImages;

            var bookTags = new List<BookTag>();
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (await _context.Tags.AnyAsync(t => t.Id == tagId))
                    {
                        bookTags.Add(new BookTag { TagId = tagId }); // Let EF mapping handle the identity
                    }
                }
            }
            book.BookTags = bookTags;

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(Guid Id)
        {
            var book = _context.Books
                .Include(b => b.BookImages)
                .Include(b => b.BookTags)
                .FirstOrDefault(b => b.Id == Id);
            if (book == null) return NotFound();
            
            book.TagIds = book.BookTags.Select(bt => bt.TagId).ToList();
            
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid Id, Book book)
        {
            if (Id != book.Id) return BadRequest();

            ModelState.Remove("Author");
            ModelState.Remove("BookImages");
            ModelState.Remove("BookTags");
            ModelState.Remove("MainImageUrl");
            ModelState.Remove("HoverImageUrl");

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _context.Authors.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View(book);
            }

            var existingBook = await _context.Books
                .Include(b => b.BookTags)
                .Include(b => b.BookImages)
                .FirstOrDefaultAsync(b => b.Id == Id);

            if (existingBook == null) return NotFound();

            if (!_context.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found");
                ViewBag.Authors = await _context.Authors.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View(book);
            }

            if (book.MainPhoto != null)
            {
                existingBook.MainImageUrl = book.MainPhoto.SaveFile("assets\\image\\products", _env.WebRootPath);
            }

            if (book.HoverPhoto != null)
            {
                existingBook.HoverImageUrl = book.HoverPhoto.SaveFile("assets\\image\\products", _env.WebRootPath);
            }

            if (book.Files != null && book.Files.Count > 0)
            {
                foreach (var file in book.Files)
                {
                    existingBook.BookImages.Add(new BookImage
                    {
                        Image = file.SaveFile("assets\\image\\products", _env.WebRootPath)
                    });
                }
            }

            // Update Tags
            _context.BookTags.RemoveRange(existingBook.BookTags);
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (await _context.Tags.AnyAsync(t => t.Id == tagId))
                    {
                        existingBook.BookTags.Add(new BookTag { TagId = tagId });
                    }
                }
            }

            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;
            existingBook.DiscountPercentage = book.DiscountPercentage;
            existingBook.InStock = book.InStock;
            existingBook.IsNew = book.IsNew;
            existingBook.IsFeatured = book.IsFeatured;
            existingBook.Code = book.Code;
            existingBook.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                .Include(b => b.BookImages)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return Json(new { success = false, message = "Id not found" });

            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Book not found" });
        }
    }
}
