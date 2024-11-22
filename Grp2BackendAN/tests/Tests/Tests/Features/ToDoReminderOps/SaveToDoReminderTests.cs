
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoReminderOps
{
    public class SaveToDoReminderTests : TestBaseCollection<IToDoReminderService> 
    {
        private readonly IValidator<AddToDoReminderReqDto> _addToDoReminderValidator;
        public SaveToDoReminderTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addToDoReminderValidator = new AddToDoReminderReqDtoValidator();
        }

        [Fact]
        public async Task SaveToDoReminderTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            ToDoMaster toDoMaster = null;
   		
            AspNetUser aspNetUser = null;
    
            int toDoReminderReminderId = 0;
            try
            {
 
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);	
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var toDoReminderReqDto = new AddToDoReminderReqDto()
                {
                    ToDoTaskId = toDoMaster.ToDoTaskId,
                    SetById = aspNetUser.Id,
                    SetForDateTime = DateTime.Now,
                    SentAtDateTime = DateTime.Now,
                    IsActive = false,

                };

                // Act
                var result = await Service.Save(toDoReminderReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                toDoReminderReminderId = (int)result.Data.ReminderId;
            }
            finally 
            {
                var toDoReminder = applicationDbContext.ToDoReminders.Find(toDoReminderReminderId);
                if (toDoReminder != null)
                {
                     await ToDoReminderHelper.CleanUp(applicationDbContext, toDoReminder);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveToDoReminderTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var toDoReminderReqDto  = new AddToDoReminderReqDto()
            {
                SetById = invalidValue,
                ToDoTaskId = 1,
                SetForDateTime = DateTime.Now,
                IsActive = default,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addToDoReminderValidator.ValidateAsync(toDoReminderReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

