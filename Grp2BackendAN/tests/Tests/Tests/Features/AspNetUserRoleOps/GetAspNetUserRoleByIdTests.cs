
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserRoleOps
{
    public class GetAspNetUserRoleByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserRoleService>(setupFixture)
    {
        [Fact]
        public async Task GetAspNetUserRoleByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            AspNetUserRole aspNetUserRole = null;
            try
            {
                aspNetUserRole = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string userId = aspNetUserRole.UserId;
                bool WithDetails = true;
                var getResult = await Service.GetById(userId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserRoleByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetUserRole aspNetUserRole = null;
            try
            {
                aspNetUserRole = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string userId = aspNetUserRole.UserId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(userId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserRoleByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string UserId = null;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(UserId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

