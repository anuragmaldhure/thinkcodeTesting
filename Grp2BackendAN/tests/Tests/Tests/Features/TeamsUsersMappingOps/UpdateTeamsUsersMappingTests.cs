
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.TeamsUsersMappingOps
{
    public class UpdateTeamsUsersMappingTests : TestBaseCollection<ITeamsUsersMappingService> 
    {
        private readonly IValidator<UpdateTeamsUsersMappingReqDto> _updateTeamsUsersMappingValidator;
        public UpdateTeamsUsersMappingTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateTeamsUsersMappingValidator = new UpdateTeamsUsersMappingReqDtoValidator();
        }

        [Fact]
        public async Task UpdateTeamsUsersMappingTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping = null;
            Team team = null;
            Team team1 = null;
            try
            {
                team = await TeamHelper.AddTeam(applicationDbContext);
                teamsUsersMapping = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateTeamsUsersMappingReqDto  = new UpdateTeamsUsersMappingReqDto()
                    {
                        TeamId = teamsUsersMapping.TeamId,
                        IsActive = false,
                        AddedAt = DateTime.Now,
                        AddedById = teamsUsersMapping.AddedById,
                    };
                team1 = teamsUsersMapping.TeamsUsersMappingTeamIdfk;
                // Act
                var updateResult = await Service.Update(updateTeamsUsersMappingReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (teamsUsersMapping != null)
                {
                    await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
				
                }
                if (team1 != null)
					await TeamHelper.CleanUp(applicationDbContext, team1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateTeamsUsersMappingTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            TeamsUsersMapping teamsUsersMapping = null;
            try
            {
                teamsUsersMapping = await TeamsUsersMappingHelper.AddTeamsUsersMapping(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateTeamsUsersMappingReqDto = new UpdateTeamsUsersMappingReqDto()
                    {
                        TeamId = teamsUsersMapping.TeamId,
                        UserId = invalidValue,
                        TeamId = teamsUsersMapping.TeamId,
                        IsActive = false,
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateTeamsUsersMappingValidator.ValidateAsync(updateTeamsUsersMappingReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (teamsUsersMapping != null)
                {
                    await TeamsUsersMappingHelper.CleanUp(applicationDbContext, teamsUsersMapping);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

