
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoMasterOps
{
    public class SaveToDoMasterTests : TestBaseCollection<IToDoMasterService> 
    {
        private readonly IValidator<AddToDoMasterReqDto> _addToDoMasterValidator;
        public SaveToDoMasterTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addToDoMasterValidator = new AddToDoMasterReqDtoValidator();
        }

        [Fact]
        public async Task SaveToDoMasterTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
    
            int toDoMasterToDoTaskId = 0;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var toDoMasterReqDto = new AddToDoMasterReqDto()
                {
                    Title = "s",
                    Description = "s",
                    IsComplete = false,
                    RepeatFrequency = "s",
                    CreatedDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CompletedDate = DateTime.Now,
                    CreatedById = aspNetUser.Id,
                    AssignedToId = "s",
                    IsAssignedToTeam = false,
                    HasChecklist = false,
                    HasReminder = false,
                    PercentageCompleted = 1.0m,
                    IsDeleted = false,
                    IsStarred = false,
                    MediaAttachmentType = "s",
                    MediaAttachmentURL = "s",

                };

                // Act
                var result = await Service.Save(toDoMasterReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                toDoMasterToDoTaskId = (int)result.Data.ToDoTaskId;
            }
            finally 
            {
                var toDoMaster = applicationDbContext.ToDoMasters.Find(toDoMasterToDoTaskId);
                if (toDoMaster != null)
                {
                     await ToDoMasterHelper.CleanUp(applicationDbContext, toDoMaster);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveToDoMasterTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 255 + 1);
            var toDoMasterReqDto  = new AddToDoMasterReqDto()
            {
                Title = invalidValue,
                Description = "string",
                IsComplete = default,
                RepeatFrequency = "string",
                CreatedDate = DateTime.Now,
                CreatedById = "string",
                AssignedToId = "string",
                IsAssignedToTeam = default,
                HasChecklist = default,
                HasReminder = default,
                PercentageCompleted = 1.0m,
                IsDeleted = default,
                IsStarred = default,
                MediaAttachmentType = "string",
                MediaAttachmentURL = "string",
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addToDoMasterValidator.ValidateAsync(toDoMasterReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

