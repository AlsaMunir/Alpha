using Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Alpha.Controllers
{
    public class CartController : Controller
    {
        
        private static List<CartItem> Cart = new List<CartItem>();
        
        public IActionResult Index()
        {
           
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            
            return View(cart);
        }
       
        [HttpPost]
        public IActionResult Show([FromForm] UserDetails u)
        {
            Console.WriteLine($"Fullname: {u.fullname}");
            Console.WriteLine($"Email: {u.mail}");
            Console.WriteLine($"Country: {u.country}");
            Console.WriteLine($"Phone: {u.phone}");

            // Reset the cart
            HttpContext.Session.SetObjectAsJson("Cart", new List<CartItem>());

            return Ok(new { message = "Order placed successfully!" });
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

           


            var existingItem = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                
                cart.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = 1,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                });
            }

            // Save cart back to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new { success = true, cartCount = cart.Sum(c => c.Quantity) });
        }
        public IActionResult CheckOut()
        {

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();


            return View(cart);
        }



    }
}
