
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamsUsersMappingOps
{
    public class SaveTeamsUsersMappingTests : TestBaseCollection<ITeamsUsersMappingService> 
    {
        private readonly IValidator<AddTeamsUsersMappingReqDto> _addTeamsUsersMappingValidator;
        public SaveTeamsUsersMappingTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addTeamsUsersMappingValidator = new AddTeamsUsersMappingReqDtoValidator();
        }

        [Fact]
        public async Task SaveTeamsUsersMappingTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            Team team = null;
   		
            AspNetUser aspNetUser = null;
   		
            AspNetUser aspNetUser = null;
    
            int teamsUsersMappingTeamId = 0;
            try
            {
 
                team = await TeamHelper.AddTeam(applicationDbContext);	
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var teamsUsersMappingReqDto = new AddTeamsUsersMappingReqDto()
                {
                    UserId = aspNetUser.Id,
                    IsActive = false,
                    AddedAt = DateTime.Now,
                    AddedById = aspNetUser.Id,

                };

                // Act
                var result = await Service.Save(teamsUsersMappingReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                teamsUsersMappingTeamId = (int)result.Data.TeamId;
            }
            finally 
            {
                var teamsUsersMapping = applicationDbContext.TeamsUsersMappings.Find(teamsUsersMappingTeamId);
                if (teamsUsersMapping != null)
                {
                     await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveTeamsUsersMappingTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var teamsUsersMappingReqDto  = new AddTeamsUsersMappingReqDto()
            {
                UserId = invalidValue,
                IsActive = default,
                AddedAt = DateTime.Now,
                AddedById = "string",
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addTeamsUsersMappingValidator.ValidateAsync(teamsUsersMappingReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

