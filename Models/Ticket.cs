using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EComWebApi.Models {
    public class Ticket
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string? Feedback { get; set; }
    }
}