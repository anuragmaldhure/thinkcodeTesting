
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoChecklistOps
{
    public class GetToDoChecklistByIdTests(SetupFixture setupFixture) : TestBaseCollection<IToDoChecklistService>(setupFixture)
    {
        [Fact]
        public async Task GetToDoChecklistByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            ToDoChecklist toDoChecklist = null;
            try
            {
                toDoChecklist = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int toDoChecklistId = toDoChecklist.ToDoChecklistId;
                bool WithDetails = true;
                var getResult = await Service.GetById(toDoChecklistId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoChecklistByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            ToDoChecklist toDoChecklist = null;
            try
            {
                toDoChecklist = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int toDoChecklistId = toDoChecklist.ToDoChecklistId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(toDoChecklistId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoChecklistByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int ToDoChecklistId = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(ToDoChecklistId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

