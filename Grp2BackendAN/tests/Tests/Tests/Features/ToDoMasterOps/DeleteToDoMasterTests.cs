
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoMasterOps
{
    public class DeleteToDoMasterTests(SetupFixture setupFixture) : TestBaseCollection<IToDoMasterService>(setupFixture)
    {
        [Fact]
        public async Task DeleteToDoMasterTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            ToDoMaster toDoMaster = null;
   		
            AspNetUser aspNetUser = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                aspNetUser = toDoMaster.ToDoMasterCreatedByIdfk;
                int toDoTaskId = toDoMaster.ToDoTaskId;

                // Act
                var deleteResult = await Service.Delete(toDoTaskId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var toDoMasterInDb = applicationDbContext.ToDoMasters.Find(toDoMaster?.ToDoTaskId);
                if (toDoMasterInDb != null)
                {
                    await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
           
                }
   		   
                if (aspNetUser != null)
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteToDoMasterTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int toDoTaskId = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(toDoTaskId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteToDoMasterCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int toDoTaskId = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(toDoTaskId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

