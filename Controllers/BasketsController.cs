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
    public class BasketsController : ControllerBase {

        private readonly EComDbContext _context;
        private readonly IConfiguration _configuration;

        public BasketsController(EComDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<BasketProduct>>> GetBasketProducts(string userId){
            return Ok(await _context.BasketProducts.Where(bp => bp.UserId == userId).ToListAsync());
            //return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketProduct(BasketProduct basketProduct){
            _context.BasketProducts.Add(basketProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketProduct(int id){
            var basketProd = await _context.BasketProducts.FindAsync(id);
            if (basketProd == null){
                return NotFound();
            }
            
            _context.BasketProducts.Remove(basketProd);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }

}