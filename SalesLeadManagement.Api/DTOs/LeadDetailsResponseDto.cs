namespace SalesLeadManagement.Api.DTOs
{
    public class LeadDetailsResponseDto
    {
        public int LeadId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string VehicleInterest { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<ActivityResponseDto> Activities { get; set; }
            = new List<ActivityResponseDto>();
    }
}