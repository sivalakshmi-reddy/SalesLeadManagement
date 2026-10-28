using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Api.Data;
using SalesLeadManagement.Api.DTOs;
using SalesLeadManagement.Api.Models;

namespace SalesLeadManagement.Api.Services
{
    public class LeadService : ILeadService
    {
        private readonly ApplicationDbContext _context;

        public LeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeadResponseDto>> GetAllLeadsAsync()
        {
            return await _context.Leads
                .AsNoTracking()
                .OrderByDescending(l => l.CreatedAt)
                .Select(l => new LeadResponseDto
                {
                    LeadId = l.LeadId,
                    CustomerName = l.CustomerName,
                    Email = l.Email,
                    Phone = l.Phone,
                    VehicleInterest = l.VehicleInterest,
                    Status = l.Status,
                    CreatedAt = l.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<LeadDetailsResponseDto?> GetLeadByIdAsync(int id)
        {
            var lead = await _context.Leads
                .AsNoTracking()
                .Include(l => l.Activities)
                .FirstOrDefaultAsync(l => l.LeadId == id);

            if (lead == null)
            {
                return null;
            }

            return new LeadDetailsResponseDto
            {
                LeadId = lead.LeadId,
                CustomerName = lead.CustomerName,
                Email = lead.Email,
                Phone = lead.Phone,
                VehicleInterest = lead.VehicleInterest,
                Status = lead.Status,
                CreatedAt = lead.CreatedAt,

                Activities = lead.Activities
                    .OrderBy(a => a.CreatedAt)
                    .ThenBy(a => a.ActivityId)
                    .Select(a => new ActivityResponseDto
                    {
                        ActivityId = a.ActivityId,
                        LeadId = a.LeadId,
                        ActivityType = a.ActivityType,
                        Description = a.Description,
                        CreatedAt = a.CreatedAt
                    })
                    .ToList()
            };
        }

        public async Task<LeadResponseDto> CreateLeadAsync(
            CreateLeadRequest request)
        {
            var lead = new Lead
            {
                CustomerName = request.CustomerName,
                Email = request.Email,
                Phone = request.Phone,
                VehicleInterest = request.VehicleInterest,
                Status = "New",
                CreatedAt = DateTime.UtcNow
            };

            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            return new LeadResponseDto
            {
                LeadId = lead.LeadId,
                CustomerName = lead.CustomerName,
                Email = lead.Email,
                Phone = lead.Phone,
                VehicleInterest = lead.VehicleInterest,
                Status = lead.Status,
                CreatedAt = lead.CreatedAt
            };
        }

        public async Task<bool> AddActivityAsync(
            int leadId,
            CreateActivityRequest request)
        {
            var leadExists = await _context.Leads
                .AnyAsync(l => l.LeadId == leadId);

            if (!leadExists)
            {
                return false;
            }

            var activity = new LeadActivity
            {
                LeadId = leadId,
                ActivityType = request.ActivityType,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.LeadActivities.Add(activity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}