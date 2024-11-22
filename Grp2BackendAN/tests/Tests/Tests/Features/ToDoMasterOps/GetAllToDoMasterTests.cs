
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoMasterOps
{
    public class GetAllToDoMastersTests(SetupFixture setupFixture) : TestBaseCollection<IToDoMasterService>(setupFixture)
    {

        [Fact]
        public async Task GetAllToDoMastersTest_ReturnsAllToDoMasters_WithoutPagination()
        {
            // Arrange
            ToDoMaster toDoMaster1 = null;
            ToDoMaster toDoMaster2 = null;
            try
            {
                toDoMaster1 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoMaster2 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
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
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllToDoMastersTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            ToDoMaster toDoMaster1 = null;
            ToDoMaster toDoMaster2 = null;
            try
            {
                toDoMaster1 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoMaster2 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoMaster1.Title;
                var getAllToDoMasterReqDto = new GetAllToDoMasterReqDto
                    {
                        Title = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllToDoMasterReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoMastersTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            ToDoMaster toDoMaster1 = null;
            ToDoMaster toDoMaster2 = null;
            try
            {
                toDoMaster1 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoMaster2 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoMaster1.ToDoTaskId + 10; 

                var getAllToDoMasterReqDto = new GetAllToDoMasterReqDto
                {
                    ToDoTaskId = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllToDoMasterReqDto);

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
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoMastersTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            ToDoMaster toDoMaster1 = null;
            ToDoMaster toDoMaster2 = null;
            try
            {
                toDoMaster1 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoMaster2 = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllToDoMasterReqDto = new GetAllToDoMasterReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllToDoMasterReqDto);

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
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


