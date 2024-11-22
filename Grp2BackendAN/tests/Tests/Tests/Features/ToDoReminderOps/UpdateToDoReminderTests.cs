
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoReminderOps
{
    public class UpdateToDoReminderTests : TestBaseCollection<IToDoReminderService> 
    {
        private readonly IValidator<UpdateToDoReminderReqDto> _updateToDoReminderValidator;
        public UpdateToDoReminderTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateToDoReminderValidator = new UpdateToDoReminderReqDtoValidator();
        }

        [Fact]
        public async Task UpdateToDoReminderTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            ToDoReminder toDoReminder = null;
            ToDoMaster toDoMaster = null;
            ToDoMaster toDoMaster1 = null;
            try
            {
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);
                toDoReminder = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateToDoReminderReqDto  = new UpdateToDoReminderReqDto()
                    {
                        ReminderId = toDoReminder.ReminderId,
                        ToDoTaskId = toDoMaster.ToDoTaskId,
                        SetById = toDoReminder.SetById,
                        SetForDateTime = DateTime.Now,
                        SentAtDateTime = DateTime.Now,
                        IsActive = false,
                    };
                toDoMaster1 = toDoReminder.ToDoReminderToDoTaskIdfk;
                // Act
                var updateResult = await Service.Update(updateToDoReminderReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (toDoReminder != null)
                {
                    await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
				
                }
                if (toDoMaster1 != null)
					await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateToDoReminderTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            ToDoReminder toDoReminder = null;
            try
            {
                toDoReminder = await ToDoReminderHelper.AddToDoReminder(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateToDoReminderReqDto = new UpdateToDoReminderReqDto()
                    {
                        ReminderId = toDoReminder.ReminderId,
                        SetById = invalidValue,
                        ToDoTaskId = toDoReminder.ToDoTaskId,
                        SetForDateTime = DateTime.Now,
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateToDoReminderValidator.ValidateAsync(updateToDoReminderReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (toDoReminder != null)
                {
                    await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

