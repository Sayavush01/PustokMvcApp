using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.Extensions;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly PustokMvcAppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokMvcAppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (slider.File == null)
            {
                ModelState.AddModelError("File", "Please select an image file.");
                return View(slider);
            }

            if (!ModelState.IsValid)
                return View(slider);

            var file = slider.File;
            
            // Check if exact same slider already exists
            bool isExist = await _context.Sliders.AnyAsync(s => 
                s.Title == slider.Title && 
                s.Description == slider.Description && 
                s.ImageUrl == file.FileName);

            if (isExist)
            {
                ModelState.AddModelError("", "A slider with the exact same Title, Description, and Image already exists!");
                return View(slider);
            }

            // Using the new FileManager extension method
            string fileName = file.SaveFile("assets\\image\\bg-images", _env.WebRootPath);

            var newSlider = new Models.Slider
            {
                Title = slider.Title,
                ButtonText = slider.ButtonText,
                ButtonUrl = slider.ButtonUrl,
                Description = slider.Description,
                ImageUrl = fileName
            };

            _context.Sliders.Add(newSlider);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
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

            if (!ModelState.IsValid)
                return View(slider);

            var existingSlider = await _context.Sliders.FindAsync(id);
            if (existingSlider == null) return NotFound();

            var file = slider.File;
            string fileNameToCheck = file != null ? file.FileName : existingSlider.ImageUrl;

            // Check if another slider with the exact same details exists
            bool isExist = await _context.Sliders.AnyAsync(s => 
                s.Id != id && 
                s.Title == slider.Title && 
                s.Description == slider.Description && 
                s.ImageUrl == fileNameToCheck);

            if (isExist)
            {
                ModelState.AddModelError("", "Another slider with the exact same Title, Description, and Image already exists!");
                return View(slider);
            }

            if (file != null)
            {
                existingSlider.ImageUrl = file.SaveFile("assets\\image\\bg-images", _env.WebRootPath);
            }

            existingSlider.Title = slider.Title;
            existingSlider.ButtonText = slider.ButtonText;
            existingSlider.ButtonUrl = slider.ButtonUrl;
            existingSlider.Description = slider.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
