
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleClaimOps
{
    public class DeleteAspNetRoleClaimTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleClaimService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetRoleClaimTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetRoleClaim aspNetRoleClaim = null;
   		
            AspNetRole aspNetRole = null;
            try
            {
                aspNetRoleClaim = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetRole = aspNetRoleClaim.AspNetRoleClaimRoleIdfk;
                int id = aspNetRoleClaim.Id;

                // Act
                var deleteResult = await Service.Delete(id);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetRoleClaimInDb = applicationDbContext.AspNetRoleClaims.Find(aspNetRoleClaim?.Id);
                if (aspNetRoleClaimInDb != null)
                {
                    await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
           
                }
   		   
                if (aspNetRole != null)
                    await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteAspNetRoleClaimTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int id = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetRoleClaimCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int id = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

