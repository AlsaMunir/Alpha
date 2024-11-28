using Microsoft.AspNetCore.Mvc;

namespace Alpha.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
