
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleClaimOps
{
    public class GetAspNetRoleClaimByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleClaimService>(setupFixture)
    {
        [Fact]
        public async Task GetAspNetRoleClaimByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            AspNetRoleClaim aspNetRoleClaim = null;
            try
            {
                aspNetRoleClaim = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int id = aspNetRoleClaim.Id;
                bool WithDetails = true;
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetRoleClaimByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetRoleClaim aspNetRoleClaim = null;
            try
            {
                aspNetRoleClaim = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int id = aspNetRoleClaim.Id;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetRoleClaimByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int Id = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(Id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

