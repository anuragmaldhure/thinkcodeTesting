
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamOps
{
    public class GetTeamByIdTests(SetupFixture setupFixture) : TestBaseCollection<ITeamService>(setupFixture)
    {
        [Fact]
        public async Task GetTeamByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            Team team = null;
            try
            {
                team = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int teamId = team.TeamId;
                bool WithDetails = true;
                var getResult = await Service.GetById(teamId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetTeamByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            Team team = null;
            try
            {
                team = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int teamId = team.TeamId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(teamId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetTeamByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int TeamId = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(TeamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

