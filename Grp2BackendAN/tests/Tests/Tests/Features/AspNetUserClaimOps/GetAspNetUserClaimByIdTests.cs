
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserClaimOps
{
    public class GetAspNetUserClaimByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserClaimService>(setupFixture)
    {
        [Fact]
        public async Task GetAspNetUserClaimByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            AspNetUserClaim aspNetUserClaim = null;
            try
            {
                aspNetUserClaim = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int id = aspNetUserClaim.Id;
                bool WithDetails = true;
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserClaimByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetUserClaim aspNetUserClaim = null;
            try
            {
                aspNetUserClaim = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int id = aspNetUserClaim.Id;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserClaimByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int Id = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(Id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

