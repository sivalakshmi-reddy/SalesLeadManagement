using System.ComponentModel.DataAnnotations;

namespace SalesLeadManagement.Api.Models
{
    public class LeadActivity
    {
        public int ActivityId { get; set; }

        public int LeadId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ActivityType { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Lead Lead { get; set; } = null!;
    }
}