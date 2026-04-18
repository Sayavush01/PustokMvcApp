using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly PustokMvcAppDbContext _context;

        public AuthorController(PustokMvcAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Author author)
        {
            if (ModelState.IsValid)
            {
                bool isExist = await _context.Authors.AnyAsync(a => a.FullName.ToLower() == author.FullName.ToLower());
                
                if (isExist)
                {
                    ModelState.AddModelError("FullName", "An author with this name already exists!");
                    return View(author);
                }

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFake(int count = 5)
        {
            var random = new Random();
            var firstNames = new[] { "John", "Jane", "Robert", "Emily", "Michael", "Sarah", "William", "Jessica", "David", "Laura" };
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Lopez" };

            for (int i = 0; i < count; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];

                _context.Authors.Add(new Models.Author
                {
                    FullName = $"{firstName} {lastName}"
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public async Task<IActionResult> DetailsModal(Guid? id)
        {
            if (id == null) return BadRequest();

            var author = await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);
            if (author == null) return NotFound();

            return PartialView("_AuthorDetailsPartial", author);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Models.Author author)
        {
            if (id != author.Id) return NotFound();

            if (ModelState.IsValid)
            {
                bool isExist = await _context.Authors.AnyAsync(a => a.Id != id && a.FullName.ToLower() == author.FullName.ToLower());

                if (isExist)
                {
                    ModelState.AddModelError("FullName", "Another author with this name already exists!");
                    return View(author);
                }

                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
