
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserRoleOps
{
    public class DeleteAspNetUserRoleTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserRoleService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetUserRoleTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetUserRole aspNetUserRole = null;
   		
            AspNetUser aspNetUser = null;
   		
            AspNetRole aspNetRole = null;
            try
            {
                aspNetUserRole = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = aspNetUserRole.AspNetUserRoleUserIdfk;
   		    
                aspNetRole = aspNetUserRole.AspNetUserRoleRoleIdfk;
                string userId = aspNetUserRole.UserId;

                // Act
                var deleteResult = await Service.Delete(userId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetUserRoleInDb = applicationDbContext.AspNetUserRoles.Find(aspNetUserRole?.UserId);
                if (aspNetUserRoleInDb != null)
                {
                    await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
   		   
                if (aspNetRole != null)
                    await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteAspNetUserRoleTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string userId = null;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(userId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetUserRoleCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            string userId = null;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(userId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

