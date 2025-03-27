using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EComWebApi.Data;
using EComWebApi.Models;

namespace EComWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase {

        private readonly EComDbContext _context;
        public WishlistsController(EComDbContext context){
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<WishList>>> GetWishlist(string userId){
            return Ok(await _context.WishLists.Where(w => w.UserId == userId).ToListAsync());
            //return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishlistItem(WishList wishListItem){
            _context.WishLists.Add(wishListItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWishlistItem(int id){
            var wItem = await _context.WishLists.FindAsync(id);
            if (wItem == null){
                return NotFound();
            }
            
            _context.WishLists.Remove(wItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}