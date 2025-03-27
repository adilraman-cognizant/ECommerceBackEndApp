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
    public class ProductsController : ControllerBase {

        private readonly EComDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(EComDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
           var product = await _context.Products.FindAsync(id);
           if (product == null)
           {
                return NotFound();
           }
           return Ok(product);
           
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<Product>> CreateProduct(Product product){

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            // Return the created product, including the generated Id
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            //return Ok(product);
        }

        [HttpPost("{id}/upload-image")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UploadImage(int id, IFormFile imageFile)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
 
            var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));
 
            var containerClient = blobServiceClient.GetBlobContainerClient("myapp-images");
            await containerClient.CreateIfNotExistsAsync();
 
           
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);
            using (var stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }
 
           
            product.ImageUrl = blobClient.Uri.ToString();
            product.ImageFileName = fileName;
            await _context.SaveChangesAsync();
 
            return Ok(new { product.ImageUrl, product.ImageFileName });
        }

        
        [HttpGet("test-blob")]
        public async Task<IActionResult> TestBlobConnectivity()

        {

        try

            {

                var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));

                var containerClient = blobServiceClient.GetBlobContainerClient("myapp-images");
        
                

                await containerClient.CreateIfNotExistsAsync();
        
            

                var blobs = new List<string>();

                await foreach (var blobItem in containerClient.GetBlobsAsync())

                {

                    blobs.Add(blobItem.Name);

                }
        
                return Ok(new { Message = "Connected to blob storage successfully", BlobList = blobs });

            }

            catch (Exception ex)

            {

                return StatusCode(500, new { Message = "Error connecting to blob storage", Error = ex.Message });

            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product){
            if (id != product.Id){
                return BadRequest("ID mis match");
            }
            Console.WriteLine("Product updated");
            _context.Products.Update(product);
            //_context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> DeleteProduct(int id){
            var product = await _context.Products.FindAsync(id);
            if (product == null){
                return NotFound();
            }
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
    
}