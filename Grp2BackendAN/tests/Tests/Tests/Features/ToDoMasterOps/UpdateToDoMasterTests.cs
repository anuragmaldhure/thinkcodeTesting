
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoMasterOps
{
    public class UpdateToDoMasterTests : TestBaseCollection<IToDoMasterService> 
    {
        private readonly IValidator<UpdateToDoMasterReqDto> _updateToDoMasterValidator;
        public UpdateToDoMasterTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateToDoMasterValidator = new UpdateToDoMasterReqDtoValidator();
        }

        [Fact]
        public async Task UpdateToDoMasterTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            ToDoMaster toDoMaster = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateToDoMasterReqDto  = new UpdateToDoMasterReqDto()
                    {
                        ToDoTaskId = toDoMaster.ToDoTaskId,
                        Title = "string",
                        Description = "string",
                        IsComplete = false,
                        RepeatFrequency = "string",
                        CreatedDate = DateTime.Now,
                        DueDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CompletedDate = DateTime.Now,
                        CreatedById = aspNetUser.Id,
                        AssignedToId = "string",
                        IsAssignedToTeam = false,
                        HasChecklist = false,
                        HasReminder = false,
                        PercentageCompleted = 1.0m,
                        IsDeleted = false,
                        IsStarred = false,
                        MediaAttachmentType = "string",
                        MediaAttachmentURL = "string",
                    };
                aspNetUser1 = toDoMaster.ToDoMasterCreatedByIdfk;
                // Act
                var updateResult = await Service.Update(updateToDoMasterReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (toDoMaster != null)
                {
                    await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateToDoMasterTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            ToDoMaster toDoMaster = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 255 + 1);
                var  updateToDoMasterReqDto = new UpdateToDoMasterReqDto()
                    {
                        ToDoTaskId = toDoMaster.ToDoTaskId,
                        Title = invalidValue,
                        Description = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateToDoMasterValidator.ValidateAsync(updateToDoMasterReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (toDoMaster != null)
                {
                    await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

