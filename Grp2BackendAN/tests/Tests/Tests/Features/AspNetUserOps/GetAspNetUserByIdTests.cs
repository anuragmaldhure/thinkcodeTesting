
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserOps
{
    public class GetAspNetUserByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserService>(setupFixture)
    {

        [Fact]
        public async Task GetAspNetUserByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string id = aspNetUser.Id;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(id, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string Id = null;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(Id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

