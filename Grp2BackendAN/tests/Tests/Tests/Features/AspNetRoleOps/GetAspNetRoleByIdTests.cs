
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleOps
{
    public class GetAspNetRoleByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleService>(setupFixture)
    {

        [Fact]
        public async Task GetAspNetRoleByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetRole aspNetRole = null;
            try
            {
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string id = aspNetRole.Id;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetRoleByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string Id = null;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(Id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

