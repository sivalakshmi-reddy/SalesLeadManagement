using Microsoft.AspNetCore.Mvc;
using SalesLeadManagement.Api.DTOs;
using SalesLeadManagement.Api.Services;

namespace SalesLeadManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeadResponseDto>>> GetAll()
        {
            var leads = await _leadService.GetAllLeadsAsync();
            return Ok(leads);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeadDetailsResponseDto>> GetById(int id)
        {
            var lead = await _leadService.GetLeadByIdAsync(id);

            if (lead == null)
            {
                return NotFound(new { message = "Lead not found." });
            }

            return Ok(lead);
        }

        [HttpPost]
        public async Task<ActionResult<LeadResponseDto>> Create(
            [FromBody] CreateLeadRequest request)
        {
            var lead = await _leadService.CreateLeadAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = lead.LeadId },
                lead);
        }

        [HttpGet("{id:int}/activities")]
        public async Task<ActionResult<List<ActivityResponseDto>>> GetActivities(
            int id)
        {
            var lead = await _leadService.GetLeadByIdAsync(id);

            if (lead == null)
            {
                return NotFound(new { message = "Lead not found." });
            }

            return Ok(lead.Activities);
        }

        [HttpPost("{id:int}/activities")]
        public async Task<IActionResult> AddActivity(
            int id,
            [FromBody] CreateActivityRequest request)
        {
            var added = await _leadService.AddActivityAsync(id, request);

            if (!added)
            {
                return NotFound(new { message = "Lead not found." });
            }

            return StatusCode(201, new
            {
                message = "Activity added successfully."
            });
        }
    }
}