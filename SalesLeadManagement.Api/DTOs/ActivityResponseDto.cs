namespace SalesLeadManagement.Api.DTOs
{
    public class ActivityResponseDto
    {
        public int ActivityId { get; set; }
        public int LeadId { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}