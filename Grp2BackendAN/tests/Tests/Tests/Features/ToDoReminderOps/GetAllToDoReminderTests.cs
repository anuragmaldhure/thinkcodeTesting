
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoReminderOps
{
    public class GetAllToDoRemindersTests(SetupFixture setupFixture) : TestBaseCollection<IToDoReminderService>(setupFixture)
    {

        [Fact]
        public async Task GetAllToDoRemindersTest_ReturnsAllToDoReminders_WithoutPagination()
        {
            // Arrange
            ToDoReminder toDoReminder1 = null;
            ToDoReminder toDoReminder2 = null;
            try
            {
                toDoReminder1 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                toDoReminder2 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
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
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder1);
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllToDoRemindersTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            ToDoReminder toDoReminder1 = null;
            ToDoReminder toDoReminder2 = null;
            try
            {
                toDoReminder1 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                toDoReminder2 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoReminder1.ToDoTaskId;
                var getAllToDoReminderReqDto = new GetAllToDoReminderReqDto
                    {
                        ToDoTaskId = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllToDoReminderReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder1);
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoRemindersTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            ToDoReminder toDoReminder1 = null;
            ToDoReminder toDoReminder2 = null;
            try
            {
                toDoReminder1 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                toDoReminder2 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = toDoReminder1.ReminderId + 10; 

                var getAllToDoReminderReqDto = new GetAllToDoReminderReqDto
                {
                    ReminderId = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllToDoReminderReqDto);

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
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder1);
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllToDoRemindersTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            ToDoReminder toDoReminder1 = null;
            ToDoReminder toDoReminder2 = null;
            try
            {
                toDoReminder1 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                toDoReminder2 = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllToDoReminderReqDto = new GetAllToDoReminderReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllToDoReminderReqDto);

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
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder1);
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


