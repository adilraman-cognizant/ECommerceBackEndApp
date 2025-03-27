using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EComWebApi.Models {
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // e.g. "Pending", "Awaiting Payment", "Paid", "Failed"
        public string Status { get; set; } = "Awaiting Payment";

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}