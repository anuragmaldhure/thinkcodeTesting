
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamOps
{
    public class UpdateTeamTests : TestBaseCollection<ITeamService> 
    {
        private readonly IValidator<UpdateTeamReqDto> _updateTeamValidator;
        public UpdateTeamTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateTeamValidator = new UpdateTeamReqDtoValidator();
        }

        [Fact]
        public async Task UpdateTeamTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            Team team = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                team = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateTeamReqDto  = new UpdateTeamReqDto()
                    {
                        TeamId = team.TeamId,
                        TeamName = "string",
                        Description = "string",
                        CreatedAt = DateTime.Now,
                        CreatedById = aspNetUser.Id,
                        IsActive = false,
                    };
                aspNetUser1 = team.TeamCreatedByIdfk;
                // Act
                var updateResult = await Service.Update(updateTeamReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (team != null)
                {
                    await TeamHelper.CleanUp(applicationDbContext, team);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateTeamTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            Team team = null;
            try
            {
                team = await TeamHelper.AddTeam(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 255 + 1);
                var  updateTeamReqDto = new UpdateTeamReqDto()
                    {
                        TeamId = team.TeamId,
                        TeamName = invalidValue,
                        Description = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateTeamValidator.ValidateAsync(updateTeamReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (team != null)
                {
                    await TeamHelper.CleanUp(applicationDbContext, team);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

