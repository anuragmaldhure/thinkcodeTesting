
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.ToDoChecklistOps
{
    public class SaveToDoChecklistTests : TestBaseCollection<IToDoChecklistService> 
    {
        private readonly IValidator<AddToDoChecklistReqDto> _addToDoChecklistValidator;
        public SaveToDoChecklistTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addToDoChecklistValidator = new AddToDoChecklistReqDtoValidator();
        }

        [Fact]
        public async Task SaveToDoChecklistTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            ToDoMaster toDoMaster = null;
    
            int toDoChecklistToDoChecklistId = 0;
            try
            {
 
                toDoMaster = await ToDoMasterHelper.AddToDoMaster(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var toDoChecklistReqDto = new AddToDoChecklistReqDto()
                {
                    Title = "s",
                    Description = "s",
                    IsComplete = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    ToDoTaskId = toDoMaster.ToDoTaskId,
                    IsDeleted = false,

                };

                // Act
                var result = await Service.Save(toDoChecklistReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                toDoChecklistToDoChecklistId = (int)result.Data.ToDoChecklistId;
            }
            finally 
            {
                var toDoChecklist = applicationDbContext.ToDoChecklists.Find(toDoChecklistToDoChecklistId);
                if (toDoChecklist != null)
                {
                     await ToDoChecklistHelper.CleanUp(applicationDbContext, toDoChecklist);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveToDoChecklistTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 255 + 1);
            var toDoChecklistReqDto  = new AddToDoChecklistReqDto()
            {
                Title = invalidValue,
                Description = "string",
                IsComplete = default,
                CreatedDate = DateTime.Now,
                ToDoTaskId = 1,
                IsDeleted = default,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addToDoChecklistValidator.ValidateAsync(toDoChecklistReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

