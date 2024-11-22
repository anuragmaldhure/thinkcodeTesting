
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoReminderOps
{
    public class DeleteToDoReminderTests(SetupFixture setupFixture) : TestBaseCollection<IToDoReminderService>(setupFixture)
    {
        [Fact]
        public async Task DeleteToDoReminderTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            ToDoReminder toDoReminder = null;
   		
            ToDoMaster toDoMaster = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                toDoReminder = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                toDoMaster = toDoReminder.ToDoReminderToDoTaskIdfk;
   		    
                aspNetUser = toDoReminder.ToDoReminderSetByIdfk;
                int reminderId = toDoReminder.ReminderId;

                // Act
                var deleteResult = await Service.Delete(reminderId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var toDoReminderInDb = applicationDbContext.ToDoReminders.Find(toDoReminder?.ReminderId);
                if (toDoReminderInDb != null)
                {
                    await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
           
                }
   		   
                if (toDoMaster != null)
                    await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteToDoReminderTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int reminderId = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(reminderId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteToDoReminderCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int reminderId = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(reminderId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

