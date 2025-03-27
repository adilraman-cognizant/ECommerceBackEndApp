using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EComWebApi.Models{
    

    // Product model will have a list of products with certain attributes
    // and each product record can have a number of reviews (review records which is another model)
    public class Product{
        [Key]
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; } 
        [Required]
        [MaxLength(50)]
        public string? Brand { get; set; } 
        [Required]
        [MaxLength(50)]
        public string? Category { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public decimal Price { get; set; } 
        [Required]
        public int Stock { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageFileName { get; set; }

    }

}