using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EComWebApi.Models{
    

    // Product model will have a list of products with certain attributes
    // and each product record can have a number of reviews (review records which is another model)
    public class BasketProduct{
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; 
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 9999)]
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
    }

}