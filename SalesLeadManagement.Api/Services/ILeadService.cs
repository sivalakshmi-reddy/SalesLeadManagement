using SalesLeadManagement.Api.DTOs;

namespace SalesLeadManagement.Api.Services
{
    public interface ILeadService
    {
        Task<List<LeadResponseDto>> GetAllLeadsAsync();

        Task<LeadDetailsResponseDto?> GetLeadByIdAsync(int id);

        Task<LeadResponseDto> CreateLeadAsync(
            CreateLeadRequest request);

        Task<bool> AddActivityAsync(
            int leadId,
            CreateActivityRequest request);
    }
}