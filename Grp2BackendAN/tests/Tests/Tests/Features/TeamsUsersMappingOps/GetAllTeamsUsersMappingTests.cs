
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamsUsersMappingOps
{
    public class GetAllTeamsUsersMappingsTests(SetupFixture setupFixture) : TestBaseCollection<ITeamsUsersMappingService>(setupFixture)
    {

        [Fact]
        public async Task GetAllTeamsUsersMappingsTest_ReturnsAllTeamsUsersMappings_WithoutPagination()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping1 = null;
            TeamsUsersMapping teamsUsersMapping2 = null;
            try
            {
                teamsUsersMapping1 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                teamsUsersMapping2 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
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
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping1);
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllTeamsUsersMappingsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping1 = null;
            TeamsUsersMapping teamsUsersMapping2 = null;
            try
            {
                teamsUsersMapping1 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                teamsUsersMapping2 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = teamsUsersMapping1.IsActive;
                var getAllTeamsUsersMappingReqDto = new GetAllTeamsUsersMappingReqDto
                    {
                        IsActive = new FilterExpression<bool>
                        {
                            Filters = new List<FilterOption<bool>> {
                                new FilterOption<bool> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllTeamsUsersMappingReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping1);
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllTeamsUsersMappingsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping1 = null;
            TeamsUsersMapping teamsUsersMapping2 = null;
            try
            {
                teamsUsersMapping1 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                teamsUsersMapping2 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = teamsUsersMapping1.TeamId + 10; 

                var getAllTeamsUsersMappingReqDto = new GetAllTeamsUsersMappingReqDto
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
                var result = await Service.GetAll(getAllTeamsUsersMappingReqDto);

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
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping1);
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllTeamsUsersMappingsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping1 = null;
            TeamsUsersMapping teamsUsersMapping2 = null;
            try
            {
                teamsUsersMapping1 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                teamsUsersMapping2 = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllTeamsUsersMappingReqDto = new GetAllTeamsUsersMappingReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllTeamsUsersMappingReqDto);

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
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping1);
                await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


