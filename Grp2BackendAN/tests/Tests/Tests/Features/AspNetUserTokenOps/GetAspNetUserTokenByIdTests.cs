
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserTokenOps
{
    public class GetAspNetUserTokenByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserTokenService>(setupFixture)
    {
        [Fact]
        public async Task GetAspNetUserTokenByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            AspNetUserToken aspNetUserToken = null;
            try
            {
                aspNetUserToken = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string userId = aspNetUserToken.UserId;
                bool WithDetails = true;
                var getResult = await Service.GetById(userId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserTokenByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetUserToken aspNetUserToken = null;
            try
            {
                aspNetUserToken = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string userId = aspNetUserToken.UserId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(userId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserTokenByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string UserId = null;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(UserId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

