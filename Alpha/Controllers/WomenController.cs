using Microsoft.AspNetCore.Mvc;
using Alpha.Models;
using Microsoft.AspNetCore.SignalR;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace Alpha.Controllers
{
    public class WomenController : Controller
    {
        private readonly ILogger<WomenController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<Women> _repository = new GenericRepos<Women>();
        public WomenController(ILogger<WomenController> logger, IWebHostEnvironment env)
        {

            _logger = logger;
            _env = env;

        }
        public IActionResult Index()
        {
            var l = _repository.GetAll();
            return View(l);
        }

        [HttpPost]
        public IActionResult Add([FromForm] Women b)
        {
            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "UploadedFiles/Women");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, b.CoverImage.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                b.CoverImage.CopyTo(fileStream);
            }

            string imagePathinroot = "~/UploadedFiles/Women/" + b.CoverImage.FileName;
            b.ImageUrl = imagePathinroot;

            // Update ModelState with new ImageUrl value
            ModelState.Clear(); // Clear existing validation
            TryValidateModel(b); // Revalidate after setting ImageUrl

            if (!ModelState.IsValid)
            {
                // Log the validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("Validation Error: " + error.ErrorMessage);
                }

                return View(b); // Return the view with validation errors
            }
            _repository.Add(b);
            return RedirectToAction("Index"); // Redirect to avoid re-submission
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string clothe)
        {
            if (string.IsNullOrEmpty(clothe))
            {
                return BadRequest();
            }
            var existing = _repository.GetAll().FirstOrDefault(x=>x.WomenOutfitName == clothe);
           
            if (existing == null)
            {
                return NotFound($"Women Outfit with name '{clothe}' not found.");
            }
            
            _repository.Delete(existing.Id);
            return View();
        }
      
    }
}
