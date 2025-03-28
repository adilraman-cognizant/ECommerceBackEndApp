using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EComWebApi.Data;
using EComWebApi.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;

namespace EComWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase {

        private readonly EComDbContext _context;
        private readonly IConfiguration _configuration;

        public ReviewsController(EComDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetProductReviews(int productId){
            return Ok(await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync());
            //return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("user/{userId}/{productId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetUserProductReview(string userId, int productId){
            return Ok(await _context.Reviews.Where(r => r.ProductId == productId && r.UserId == userId).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductReview(Review review){
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id){
            var review = await _context.Reviews.FindAsync(id);
            if (review == null){
                return NotFound();
            }
            
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, Review review){
            if (id != review.Id){
                return BadRequest("ID mis match");
            }
            Console.WriteLine("Review updated");
            _context.Reviews.Update(review);
            //_context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

    }

}