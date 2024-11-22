
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserLoginOps
{
    public class GetAspNetUserLoginByIdTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserLoginService>(setupFixture)
    {
        [Fact]
        public async Task GetAspNetUserLoginByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            AspNetUserLogin aspNetUserLogin = null;
            try
            {
                aspNetUserLogin = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string loginProvider = aspNetUserLogin.LoginProvider;
                bool WithDetails = true;
                var getResult = await Service.GetById(loginProvider, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserLoginByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            AspNetUserLogin aspNetUserLogin = null;
            try
            {
                aspNetUserLogin = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string loginProvider = aspNetUserLogin.LoginProvider;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(loginProvider, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAspNetUserLoginByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string LoginProvider = null;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(LoginProvider);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

