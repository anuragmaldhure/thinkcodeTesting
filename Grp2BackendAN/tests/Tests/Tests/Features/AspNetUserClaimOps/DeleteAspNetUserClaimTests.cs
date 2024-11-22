
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserClaimOps
{
    public class DeleteAspNetUserClaimTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserClaimService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetUserClaimTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetUserClaim aspNetUserClaim = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUserClaim = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = aspNetUserClaim.AspNetUserClaimUserIdfk;
                int id = aspNetUserClaim.Id;

                // Act
                var deleteResult = await Service.Delete(id);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetUserClaimInDb = applicationDbContext.AspNetUserClaims.Find(aspNetUserClaim?.Id);
                if (aspNetUserClaimInDb != null)
                {
                    await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteAspNetUserClaimTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int id = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetUserClaimCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int id = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

