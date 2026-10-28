using System.ComponentModel.DataAnnotations;

namespace SalesLeadManagement.Api.DTOs
{
    public class CreateActivityRequest
    {
        [Required]
        [MaxLength(30)]
        public string ActivityType { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}