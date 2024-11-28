using Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alpha.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShopCart()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }


    }
}
