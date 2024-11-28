using Microsoft.AspNetCore.Mvc;
using Alpha.Models;
using Microsoft.AspNetCore.SignalR;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace Alpha.Controllers
{
    public class SkinCareController : Controller
    {
        private readonly ILogger<SkinCareController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<SkinCare> _repository = new GenericRepos<SkinCare>();
        public SkinCareController(ILogger<SkinCareController> logger, IWebHostEnvironment env)
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
        public IActionResult Add([FromForm] SkinCare b)
        {
            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "UploadedFiles/SkinCare");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, b.CoverImage.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                b.CoverImage.CopyTo(fileStream);
            }

            string imagePathinroot = "~/UploadedFiles/SkinCare/" + b.CoverImage.FileName;
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
            var existing = _repository.GetAll().FirstOrDefault(x => x.SkinCareName == clothe);

            if (existing == null)
            {
                return NotFound($"SkinCare Outfit with name '{clothe}' not found.");
            }

            _repository.Delete(existing.Id);
            return View();
        }

    }
}
