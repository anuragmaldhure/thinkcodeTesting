
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamOps
{
    public class SaveTeamTests : TestBaseCollection<ITeamService> 
    {
        private readonly IValidator<AddTeamReqDto> _addTeamValidator;
        public SaveTeamTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addTeamValidator = new AddTeamReqDtoValidator();
        }

        [Fact]
        public async Task SaveTeamTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
    
            int teamTeamId = 0;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var teamReqDto = new AddTeamReqDto()
                {
                    TeamName = "s",
                    Description = "s",
                    CreatedAt = DateTime.Now,
                    CreatedById = aspNetUser.Id,
                    IsActive = false,

                };

                // Act
                var result = await Service.Save(teamReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                teamTeamId = (int)result.Data.TeamId;
            }
            finally 
            {
                var team = applicationDbContext.Teams.Find(teamTeamId);
                if (team != null)
                {
                     await TeamHelper.CleanUp(applicationDbContext, team);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveTeamTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 255 + 1);
            var teamReqDto  = new AddTeamReqDto()
            {
                TeamName = invalidValue,
                Description = "string",
                CreatedAt = DateTime.Now,
                CreatedById = "string",
                IsActive = default,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addTeamValidator.ValidateAsync(teamReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

