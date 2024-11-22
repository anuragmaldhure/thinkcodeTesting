
using thinkbridge.Grp2BackendAN.Tests.Helpers;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoChecklistOps
{
    public class DeleteToDoChecklistTests(SetupFixture setupFixture) : TestBaseCollection<IToDoChecklistService>(setupFixture)
    {
        [Fact]
        public async Task DeleteToDoChecklistTest_ReturnsSuccessResult_WhenProvidedValidId()
        {
            ToDoChecklist toDoChecklist = null;
   		
            ToDoMaster toDoMaster = null;
            try
            {
                toDoChecklist = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
   		    
                toDoMaster = toDoChecklist.ToDoChecklistToDoTaskIdfk;
                int toDoChecklistId = toDoChecklist.ToDoChecklistId;

                // Act
                var deleteResult = await Service.Delete(toDoChecklistId);
                // Assert
                deleteResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                var toDoChecklistInDb = applicationDbContext.ToDoChecklists.Find(toDoChecklist?.ToDoChecklistId);
                if (toDoChecklistInDb != null)
                {
                    await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
           
                }
   		   
                if (toDoMaster != null)
                    await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
 
					await applicationDbContext.SaveChangesAsync();
 
            }
        }

        [Fact]
        public async Task DeleteToDoChecklistTest_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int toDoChecklistId = 0;

            // Act & Assert
            Func<Task> act = async () => await Service.Delete(toDoChecklistId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteToDoChecklistCommand_ReturnsFailureResult_WhenProvidedNullValueForId()
        {
            // Arrange
            int toDoChecklistId = 0;
            // Act & Assert
            Func<Task> act = async () => await Service.Delete(toDoChecklistId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}

