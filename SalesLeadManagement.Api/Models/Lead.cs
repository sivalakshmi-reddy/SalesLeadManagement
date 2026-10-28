using System.ComponentModel.DataAnnotations;

namespace SalesLeadManagement.Api.Models
{
    public class Lead
    {
        public int LeadId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string VehicleInterest { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Status { get; set; } = "New";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<LeadActivity> Activities { get; set; }
            = new List<LeadActivity>();
    }
}