using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EComWebApi.Data;
using EComWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EComWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly EComDbContext _context;

        public ProfilesController(EComDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int userId)
        {
            var userProfile = await _context.UserProfiles.FindAsync(userId);
            if (userProfile == null)
            {
                return NotFound(); // Return 404 if user profile does not exist
            }
            return Ok(userProfile);
        }

    }
}
