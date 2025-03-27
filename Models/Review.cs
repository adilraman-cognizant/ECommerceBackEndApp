using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EComWebApi.Models{
    
    // A review record will belong to a particular user and product
    public class Review{

        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; 
        [Required]
        public int ProductId { get; set; } 

        [Required]
        public int Rating { get; set; } 

        [MaxLength(300)]
        [DefaultValue("No Comment provided")]
        public string? Comment { get; set; }
    }

}