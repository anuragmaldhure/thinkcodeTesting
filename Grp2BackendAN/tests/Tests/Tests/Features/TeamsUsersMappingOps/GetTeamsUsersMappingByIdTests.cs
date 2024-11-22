
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamsUsersMappingOps
{
    public class GetTeamsUsersMappingByIdTests(SetupFixture setupFixture) : TestBaseCollection<ITeamsUsersMappingService>(setupFixture)
    {
        [Fact]
        public async Task GetTeamsUsersMappingByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            TeamsUsersMapping teamsUsersMapping = null;
            try
            {
                teamsUsersMapping = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int teamId = teamsUsersMapping.TeamId;
                bool WithDetails = true;
                var getResult = await Service.GetById(teamId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetTeamsUsersMappingByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            TeamsUsersMapping teamsUsersMapping = null;
            try
            {
                teamsUsersMapping = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int teamId = teamsUsersMapping.TeamId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(teamId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetTeamsUsersMappingByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int TeamId = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(TeamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

