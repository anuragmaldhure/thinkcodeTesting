
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserOps
{
    public class DeleteAspNetUserTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserService>(setupFixture)
    {
        [Fact]
        public async Task DeleteAspNetUserTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string id = aspNetUser.Id;

                // Act
                var deleteResult = await Service.Delete(id);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var aspNetUserInDb = applicationDbContext.AspNetUsers.Find(aspNetUser?.Id);
                if (aspNetUserInDb != null)
                {
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
           
                }
 
            }
        }

        [Fact]
        public async Task DeleteAspNetUserTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            string id = null;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAspNetUserCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            string id = null;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(id);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

