using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly PustokMvcAppDbContext _context;

        public SliderController(PustokMvcAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public async Task<IActionResult> GenerateFake(int count = 5)
        {
            var random = new Random();
            // Using actual image filenames that exist in your wwwroot/assets/image/bg-images/ directory
            var images = new[] { "home-2-slider-1.jpg", "home-2-slider-2.jpg", "home-3-slider-1.jpg", "home-3-slider-2.jpg", "home-4-slider-1.jpg", "home-slider-1-ai.png" };
            var titles = new[] { "Huge Summer Sale!", "New Arrivals 2024", "Top Rated Books", "Discover Knowledge", "Best Sellers Collection" };
            var btnTexts = new[] { "Shop Now", "Learn More", "Explore", "View All", "Read More" };

            for (int i = 0; i < count; i++)
            {
                _context.Sliders.Add(new Models.Slider
                {
                    ImageUrl = images[random.Next(images.Length)],
                    Title = titles[random.Next(titles.Length)],
                    Description = "This is an auto-generated slider description summarizing the content.",
                    ButtonText = btnTexts[random.Next(btnTexts.Length)],
                    ButtonUrl = "#"
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Sliders.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Slider slider)
        {
            if (id != slider.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return Json(new { success = false, message = "Id not found" });

            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Slider not found" });
        }
    }
}
