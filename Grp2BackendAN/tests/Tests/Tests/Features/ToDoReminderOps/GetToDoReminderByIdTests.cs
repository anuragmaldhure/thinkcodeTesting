
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoReminderOps
{
    public class GetToDoReminderByIdTests(SetupFixture setupFixture) : TestBaseCollection<IToDoReminderService>(setupFixture)
    {
        [Fact]
        public async Task GetToDoReminderByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            ToDoReminder toDoReminder = null;
            try
            {
                toDoReminder = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int reminderId = toDoReminder.ReminderId;
                bool WithDetails = true;
                var getResult = await Service.GetById(reminderId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoReminderByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            ToDoReminder toDoReminder = null;
            try
            {
                toDoReminder = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int reminderId = toDoReminder.ReminderId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(reminderId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoReminderByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int ReminderId = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(ReminderId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

