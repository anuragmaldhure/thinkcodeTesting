
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserClaimOps
{
    public class UpdateAspNetUserClaimTests : TestBaseCollection<IAspNetUserClaimService> 
    {
        private readonly IValidator<UpdateAspNetUserClaimReqDto> _updateAspNetUserClaimValidator;
        public UpdateAspNetUserClaimTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetUserClaimValidator = new UpdateAspNetUserClaimReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetUserClaimTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUserClaim = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetUserClaimReqDto  = new UpdateAspNetUserClaimReqDto()
                    {
                        Id = aspNetUserClaim.Id,
                        UserId = aspNetUser.Id,
                        ClaimType = "string",
                        ClaimValue = "string",
                    };
                aspNetUser1 = aspNetUserClaim.AspNetUserClaimUserIdfk;
                // Act
                var updateResult = await Service.Update(updateAspNetUserClaimReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetUserClaim != null)
                {
                    await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateAspNetUserClaimTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim = null;
            try
            {
                aspNetUserClaim = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetUserClaimReqDto = new UpdateAspNetUserClaimReqDto()
                    {
                        Id = aspNetUserClaim.Id,
                        UserId = invalidValue,
                        ClaimType = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetUserClaimValidator.ValidateAsync(updateAspNetUserClaimReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetUserClaim != null)
                {
                    await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

