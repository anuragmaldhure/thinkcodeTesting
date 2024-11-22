
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamOps
{
    public class DeleteTeamTests(SetupFixture setupFixture) : TestBaseCollection<ITeamService>(setupFixture)
    {
        [Fact]
        public async Task DeleteTeamTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            Team team = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                team = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = team.TeamCreatedByIdfk;
                int teamId = team.TeamId;

                // Act
                var deleteResult = await Service.Delete(teamId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var teamInDb = applicationDbContext.Teams.Find(team?.TeamId);
                if (teamInDb != null)
                {
                    await TeamHelper.CleanUp(applicationDbContext, team);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteTeamTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int teamId = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(teamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteTeamCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int teamId = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(teamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

