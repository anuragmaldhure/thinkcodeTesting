
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserTokenOps
{
    public class DeleteAspNetUserTokenTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserTokenService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetUserTokenTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetUserToken aspNetUserToken = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUserToken = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = aspNetUserToken.AspNetUserTokenUserIdfk;
                string userId = aspNetUserToken.UserId;

                // Act
                var deleteResult = await Service.Delete(userId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetUserTokenInDb = applicationDbContext.AspNetUserTokens.Find(aspNetUserToken?.UserId);
                if (aspNetUserTokenInDb != null)
                {
                    await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteAspNetUserTokenTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string userId = null;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(userId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetUserTokenCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            string userId = null;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(userId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

