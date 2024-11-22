
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamsUsersMappingOps
{
    public class DeleteTeamsUsersMappingTests(SetupFixture setupFixture) : TestBaseCollection<ITeamsUsersMappingService>(setupFixture)
    {
        [Fact]
        public async Task DeleteTeamsUsersMappingTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            TeamsUsersMapping teamsUsersMapping = null;
   		
            Team team = null;
   		
            AspNetUser aspNetUser = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                teamsUsersMapping = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                team = teamsUsersMapping.TeamsUsersMappingTeamIdfk;
   		    
                aspNetUser = teamsUsersMapping.TeamsUsersMappingUserIdfk;
   		    
                aspNetUser = teamsUsersMapping.TeamsUsersMappingAddedByIdfk;
                int teamId = teamsUsersMapping.TeamId;

                // Act
                var deleteResult = await Service.Delete(teamId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var teamsUsersMappingInDb = applicationDbContext.TeamsUsersMappings.Find(teamsUsersMapping?.TeamId);
                if (teamsUsersMappingInDb != null)
                {
                    await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
           
                }
   		   
                if (team != null)
                    await TeamHelper.CleanUp(applicationDbContext, team);
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteTeamsUsersMappingTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int teamId = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(teamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteTeamsUsersMappingCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int teamId = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(teamId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

