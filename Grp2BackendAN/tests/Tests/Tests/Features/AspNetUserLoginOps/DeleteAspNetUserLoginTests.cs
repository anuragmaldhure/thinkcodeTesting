
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserLoginOps
{
    public class DeleteAspNetUserLoginTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserLoginService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetUserLoginTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetUserLogin aspNetUserLogin = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUserLogin = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = aspNetUserLogin.AspNetUserLoginUserIdfk;
                string loginProvider = aspNetUserLogin.LoginProvider;

                // Act
                var deleteResult = await Service.Delete(loginProvider);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetUserLoginInDb = applicationDbContext.AspNetUserLogins.Find(aspNetUserLogin?.LoginProvider);
                if (aspNetUserLoginInDb != null)
                {
                    await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteAspNetUserLoginTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string loginProvider = null;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(loginProvider);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetUserLoginCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            string loginProvider = null;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(loginProvider);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

