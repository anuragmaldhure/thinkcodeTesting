
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamOps
{
    public class GetAllTeamsTests(SetupFixture setupFixture) : TestBaseCollection<ITeamService>(setupFixture)
    {

        [Fact]
        public async Task GetAllTeamsTest_ReturnsAllTeams_WithoutPagination()
        {
            // Arrange
            Team team1 = null;
            Team team2 = null;
            try
            {
                team1 = await TeamHelper.AddTeam(applicationDbContext);
                team2 = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                // Act
                var result = await Service.GetAll(null);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team1);
                await TeamHelper.CleanUp(applicationDbContext, team2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllTeamsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            Team team1 = null;
            Team team2 = null;
            try
            {
                team1 = await TeamHelper.AddTeam(applicationDbContext);
                team2 = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = team1.TeamName;
                var getAllTeamReqDto = new GetAllTeamReqDto
                    {
                        TeamName = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllTeamReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team1);
                await TeamHelper.CleanUp(applicationDbContext, team2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllTeamsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            Team team1 = null;
            Team team2 = null;
            try
            {
                team1 = await TeamHelper.AddTeam(applicationDbContext);
                team2 = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = team1.TeamId + 10; 

                var getAllTeamReqDto = new GetAllTeamReqDto
                {
                    TeamId = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllTeamReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                result.Data.Should().BeEmpty();
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team1);
                await TeamHelper.CleanUp(applicationDbContext, team2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllTeamsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            Team team1 = null;
            Team team2 = null;
            try
            {
                team1 = await TeamHelper.AddTeam(applicationDbContext);
                team2 = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllTeamReqDto = new GetAllTeamReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllTeamReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                result.Data.Should().HaveCount(1);
            }
            catch (Exception ex)
            {
                // Handle exception or fail the test
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await TeamHelper.CleanUp(applicationDbContext, team1);
                await TeamHelper.CleanUp(applicationDbContext, team2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


