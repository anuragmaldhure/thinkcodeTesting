
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleClaimOps
{
    public class UpdateAspNetRoleClaimTests : TestBaseCollection<IAspNetRoleClaimService> 
    {
        private readonly IValidator<UpdateAspNetRoleClaimReqDto> _updateAspNetRoleClaimValidator;
        public UpdateAspNetRoleClaimTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetRoleClaimValidator = new UpdateAspNetRoleClaimReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetRoleClaimTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim = null;
            AspNetRole aspNetRole = null;
            AspNetRole aspNetRole1 = null;
            try
            {
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                aspNetRoleClaim = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetRoleClaimReqDto  = new UpdateAspNetRoleClaimReqDto()
                    {
                        Id = aspNetRoleClaim.Id,
                        RoleId = aspNetRole.Id,
                        ClaimType = "string",
                        ClaimValue = "string",
                    };
                aspNetRole1 = aspNetRoleClaim.AspNetRoleClaimRoleIdfk;
                // Act
                var updateResult = await Service.Update(updateAspNetRoleClaimReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetRoleClaim != null)
                {
                    await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
				
                }
                if (aspNetRole1 != null)
					await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateAspNetRoleClaimTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim = null;
            try
            {
                aspNetRoleClaim = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetRoleClaimReqDto = new UpdateAspNetRoleClaimReqDto()
                    {
                        Id = aspNetRoleClaim.Id,
                        RoleId = invalidValue,
                        ClaimType = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetRoleClaimValidator.ValidateAsync(updateAspNetRoleClaimReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetRoleClaim != null)
                {
                    await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

