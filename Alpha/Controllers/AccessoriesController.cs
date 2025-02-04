﻿using Microsoft.AspNetCore.Mvc;
using Alpha.Models;
using Microsoft.AspNetCore.SignalR;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace Alpha.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly ILogger<AccessoriesController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<Accessories> _repository = new GenericRepos<Accessories>();
        public AccessoriesController(ILogger<AccessoriesController> logger, IWebHostEnvironment env)
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
        public IActionResult Add([FromForm] Accessories b)
        {
            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "UploadedFiles/Accessories");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, b.CoverImage.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                b.CoverImage.CopyTo(fileStream);
            }

            string imagePathinroot = "~/UploadedFiles/Accessories/" + b.CoverImage.FileName;
            b.ImageUrl = imagePathinroot;

            
            ModelState.Clear(); 
            TryValidateModel(b); 

            if (!ModelState.IsValid)
            {
              
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("Validation Error: " + error.ErrorMessage);
                }

                return View(b);
            }
            _repository.Add(b);
            return RedirectToAction("Index"); 
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
            var existing = _repository.GetAll().FirstOrDefault(x => x.AccessoriesName == clothe);

            if (existing == null)
            {
                return NotFound($"Accessories Outfit with name '{clothe}' not found.");
            }

            _repository.Delete(existing.Id);
            return View();
        }

    }
}
