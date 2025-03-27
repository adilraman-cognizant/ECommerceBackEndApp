using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EComWebApi.Data;
using EComWebApi.Models;
using Azure.Storage.Blobs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EComWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {

        private readonly EComDbContext _context;
        private readonly IConfiguration _configuration;

        public OrdersController(EComDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders(string userId)
        {
            Console.WriteLine($"Order API -> User id retrieved: {userId}");
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
            if (orders.Count == 0)
                return NotFound("No orders found.");

            return Ok(orders);
        }

        [HttpPost("markpaid/{orderId}")]
        [Authorize]
        public async Task<IActionResult> MarkPaid(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound($"Order {orderId} not found.");

            
            // If it's already paid, do nothing
            if (order.Status == "Paid")
                return Ok("Order is already paid.");

            // Mark order as paid
            order.Status = "Paid";

            // Remove the user's cart items (if any remain)
            var userBasket = _context.BasketProducts
                .Where(bp => bp.UserId == order.UserId)
                .ToList();
            _context.BasketProducts.RemoveRange(userBasket);

            // Reduce product stock for each item in this order
            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock = Math.Max(0, product.Stock - item.Quantity);
                }
            }

            await _context.SaveChangesAsync();
            return Ok($"Order {orderId} marked as Paid. Cart cleared, stock reduced.");
        }


    }
}