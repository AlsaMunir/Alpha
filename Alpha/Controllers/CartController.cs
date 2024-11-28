using Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Alpha.Controllers
{
    public class CartController : Controller
    {
        // Simulate a database or session-based cart
        private static List<CartItem> Cart = new List<CartItem>();
        public IActionResult Index()
{
    // Retrieve the cart from the session (or initialize a new one if it doesn't exist)
    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

    // Pass the cart to the view
    return View(cart);
}


        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            // Retrieve cart from session or initialize a new one
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Check if the item is already in the cart
            var existingItem = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                // Add new item to the cart
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

    }
}
