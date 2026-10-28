using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Api.Data;
using SalesLeadManagement.Api.DTOs;
using SalesLeadManagement.Api.Services;
using Xunit;

namespace SalesLeadManagement.Tests
{
    public class LeadServiceTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateLeadAsync_ShouldSaveLead()
        {
            using var context = CreateContext();
            var service = new LeadService(context);

            var request = new CreateLeadRequest
            {
                CustomerName = "Test Customer",
                Email = "test@example.com",
                Phone = "9000000001",
                VehicleInterest = "Hyundai Creta"
            };

            var result = await service.CreateLeadAsync(request);

            Assert.True(result.LeadId > 0);
            Assert.Equal("Test Customer", result.CustomerName);
            Assert.Equal("New", result.Status);
            Assert.Single(await context.Leads.ToListAsync());
        }

        [Fact]
        public async Task GetAllLeadsAsync_ShouldReturnSavedLeads()
        {
            using var context = CreateContext();
            var service = new LeadService(context);

            await service.CreateLeadAsync(new CreateLeadRequest
            {
                CustomerName = "Customer One",
                Email = "one@example.com",
                VehicleInterest = "Hyundai Creta"
            });

            var result = await service.GetAllLeadsAsync();

            Assert.Single(result);
            Assert.Equal("Customer One", result[0].CustomerName);
        }

        [Fact]
        public async Task AddActivityAsync_ShouldSaveActivity()
        {
            using var context = CreateContext();
            var service = new LeadService(context);

            var lead = await service.CreateLeadAsync(new CreateLeadRequest
            {
                CustomerName = "Test Customer",
                Email = "test@example.com",
                VehicleInterest = "Hyundai Creta"
            });

            var added = await service.AddActivityAsync(
                lead.LeadId,
                new CreateActivityRequest
                {
                    ActivityType = "Call",
                    Description = "Discussed vehicle pricing."
                });

            var details = await service.GetLeadByIdAsync(lead.LeadId);

            Assert.True(added);
            Assert.NotNull(details);
            Assert.Single(details.Activities);
            Assert.Equal("Call", details.Activities[0].ActivityType);
        }

        [Fact]
        public async Task AddActivityAsync_ShouldReturnFalseForMissingLead()
        {
            using var context = CreateContext();
            var service = new LeadService(context);

            var added = await service.AddActivityAsync(
                999,
                new CreateActivityRequest
                {
                    ActivityType = "Call",
                    Description = "Test activity."
                });

            Assert.False(added);
            Assert.Empty(await context.LeadActivities.ToListAsync());
        }

        [Fact]
        public async Task GetLeadByIdAsync_ShouldReturnNullForMissingLead()
        {
            using var context = CreateContext();
            var service = new LeadService(context);

            var result = await service.GetLeadByIdAsync(999);

            Assert.Null(result);
        }
    }
}