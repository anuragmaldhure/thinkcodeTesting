
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleOps
{
    public class DeleteAspNetRoleTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetRoleTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetRole aspNetRole = null;
            try
            {
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string id = aspNetRole.Id;

                // Act
                var deleteResult = await Service.Delete(id);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetRoleInDb = applicationDbContext.AspNetRoles.Find(aspNetRole?.Id);
                if (aspNetRoleInDb != null)
                {
                    await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
 
					await applicationDbContext.SaveChangesAsync();
           
                }
 
            }
        }

        [Fact]
        public async Task DeleteAspNetRoleTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string id = null;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetRoleCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            string id = null;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

