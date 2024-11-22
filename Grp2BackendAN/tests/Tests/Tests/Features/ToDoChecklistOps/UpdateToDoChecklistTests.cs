
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoChecklistOps
{
    public class UpdateToDoChecklistTests : TestBaseCollection<IToDoChecklistService> 
    {
        private readonly IValidator<UpdateToDoChecklistReqDto> _updateToDoChecklistValidator;
        public UpdateToDoChecklistTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateToDoChecklistValidator = new UpdateToDoChecklistReqDtoValidator();
        }

        [Fact]
        public async Task UpdateToDoChecklistTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            ToDoChecklist toDoChecklist = null;
            ToDoMaster toDoMaster = null;
            ToDoMaster toDoMaster1 = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoChecklist = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateToDoChecklistReqDto  = new UpdateToDoChecklistReqDto()
                    {
                        ToDoChecklistId = toDoChecklist.ToDoChecklistId,
                        Title = "string",
                        Description = "string",
                        IsComplete = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        ToDoTaskId = toDoMaster.ToDoTaskId,
                        IsDeleted = false,
                    };
                toDoMaster1 = toDoChecklist.ToDoChecklistToDoTaskIdfk;
                // Act
                var updateResult = await Service.Update(updateToDoChecklistReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (toDoChecklist != null)
                {
                    await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
				
                }
                if (toDoMaster1 != null)
					await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateToDoChecklistTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            ToDoChecklist toDoChecklist = null;
            try
            {
                toDoChecklist = await ToDoChecklistHelper.AddToDoChecklist(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 255 + 1);
                var  updateToDoChecklistReqDto = new UpdateToDoChecklistReqDto()
                    {
                        ToDoChecklistId = toDoChecklist.ToDoChecklistId,
                        Title = invalidValue,
                        Description = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateToDoChecklistValidator.ValidateAsync(updateToDoChecklistReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (toDoChecklist != null)
                {
                    await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

