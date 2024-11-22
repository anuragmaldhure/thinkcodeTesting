
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoMasterOps
{
    public class GetToDoMasterByIdTests(SetupFixture setupFixture) : TestBaseCollection<IToDoMasterService>(setupFixture)
    {
        [Fact]
        public async Task GetToDoMasterByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithDetails()
        {
            ToDoMaster toDoMaster = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int toDoTaskId = toDoMaster.ToDoTaskId;
                bool WithDetails = true;
                var getResult = await Service.GetById(toDoTaskId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoMasterByIdTest_ReturnsSuccessResult_WhenProvidedValidIdGetWithoutDetails()
        {
            ToDoMaster toDoMaster = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                int toDoTaskId = toDoMaster.ToDoTaskId;
                bool WithDetails = false;
                
                var getResult = await Service.GetById(toDoTaskId, WithDetails);

                // Assert
                getResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally
            {
                await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetToDoMasterByIdQuery_ReturnsFailureResult_WhenProvidedInvalidId()
        {
            // Arrange
            int ToDoTaskId = 0;
 

            // Act & Assert
            Func<Task> act = async () => await Service.GetById(ToDoTaskId);
            await act.Should().ThrowAsync<NotFoundException>();
        }
        
    }
}

