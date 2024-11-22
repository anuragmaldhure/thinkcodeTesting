
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserClaimOps
{
    public class SaveAspNetUserClaimTests : TestBaseCollection<IAspNetUserClaimService> 
    {
        private readonly IValidator<AddAspNetUserClaimReqDto> _addAspNetUserClaimValidator;
        public SaveAspNetUserClaimTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetUserClaimValidator = new AddAspNetUserClaimReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetUserClaimTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
    
            int aspNetUserClaimId = 0;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var aspNetUserClaimReqDto = new AddAspNetUserClaimReqDto()
                {
                    UserId = aspNetUser.Id,
                    ClaimType = "s",
                    ClaimValue = "s",

                };

                // Act
                var result = await Service.Save(aspNetUserClaimReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetUserClaimId = (int)result.Data.Id;
            }
            finally 
            {
                var aspNetUserClaim = applicationDbContext.AspNetUserClaims.Find(aspNetUserClaimId);
                if (aspNetUserClaim != null)
                {
                     await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetUserClaimTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetUserClaimReqDto  = new AddAspNetUserClaimReqDto()
            {
                UserId = invalidValue,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetUserClaimValidator.ValidateAsync(aspNetUserClaimReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

