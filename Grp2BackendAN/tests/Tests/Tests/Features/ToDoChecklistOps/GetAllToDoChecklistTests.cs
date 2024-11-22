
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoChecklistOps
{
    public class GetAllToDoChecklistsTests(SetupFixture setupFixture) : TestBaseCollection<IToDoChecklistService>(setupFixture)
    {

        [Fact]
        public async Task GetAllToDoChecklistsTest_ReturnsAllToDoChecklists_WithoutPagination()
        {
            // Arrange
            ToDoChecklist toDoChecklist1 = null;
            ToDoChecklist toDoChecklist2 = null;
            try
            {
                toDoChecklist1 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                toDoChecklist2 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
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
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist1);
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllToDoChecklistsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            ToDoChecklist toDoChecklist1 = null;
            ToDoChecklist toDoChecklist2 = null;
            try
            {
                toDoChecklist1 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                toDoChecklist2 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoChecklist1.Title;
                var getAllToDoChecklistReqDto = new GetAllToDoChecklistReqDto
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
                var result = await Service.GetAll(getAllToDoChecklistReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist1);
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoChecklistsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            ToDoChecklist toDoChecklist1 = null;
            ToDoChecklist toDoChecklist2 = null;
            try
            {
                toDoChecklist1 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                toDoChecklist2 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoChecklist1.ToDoChecklistId + 10; 

                var getAllToDoChecklistReqDto = new GetAllToDoChecklistReqDto
                {
                    ToDoChecklistId = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllToDoChecklistReqDto);

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
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist1);
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoChecklistsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            ToDoChecklist toDoChecklist1 = null;
            ToDoChecklist toDoChecklist2 = null;
            try
            {
                toDoChecklist1 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                toDoChecklist2 = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllToDoChecklistReqDto = new GetAllToDoChecklistReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllToDoChecklistReqDto);

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
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist1);
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


