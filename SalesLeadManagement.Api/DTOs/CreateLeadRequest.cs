using System.ComponentModel.DataAnnotations;

namespace SalesLeadManagement.Api.DTOs
{
    public class CreateLeadRequest
    {
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
    }
}