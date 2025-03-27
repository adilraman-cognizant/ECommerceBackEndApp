using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EComWebApi.Data;
using EComWebApi.Models;

namespace EComWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayPalController : ControllerBase
    {
        private readonly EComDbContext _context;

        public PayPalController(EComDbContext context)
        {
            _context = context;
        }

        // POST /api/paypal/create-order
        // Creates an order from the user's cart, sets status = "Awaiting Payment"
        // DOES NOT remove cart items yet
        [HttpPost("create-order/{userId}")]
        [Authorize]
        public async Task<IActionResult> CreatePayPalOrder(string userId)
        {
            
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            Console.WriteLine($"User id retrieved: {userId}");
            // Get cart items
            var basketItems = await _context.BasketProducts
                .Where(bp => bp.UserId == userId)
                .ToListAsync();

            if (basketItems.Count == 0)
            {
                return BadRequest("Cart is empty. Cannot create an order.");
            }

            // Make sure user can't create a new order if they have any unpaid orders
            var unpaidOrders = await _context.Orders
                .Where(o => o.UserId == userId && o.Status == "Awaiting Payment")
                .ToListAsync();
            if (unpaidOrders.Count > 0)
            {
                return BadRequest("You have unpaid orders. Please pay for them first.");
            }

            // Create a new order with status "Awaiting Payment"
            var order = new Order
            {
                UserId = userId,
                Status = "Awaiting Payment",
                CreatedAt = DateTime.UtcNow,
                // Build order items from the cart
                OrderItems = basketItems.Select(bi => new OrderItem
                {
                    ProductId = bi.ProductId,
                    Quantity = bi.Quantity,
                    UnitPrice = bi.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Return the new order ID to the client
            return Ok(order.Id);
        }
    }
}