
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleClaimOps
{
    public class SaveAspNetRoleClaimTests : TestBaseCollection<IAspNetRoleClaimService> 
    {
        private readonly IValidator<AddAspNetRoleClaimReqDto> _addAspNetRoleClaimValidator;
        public SaveAspNetRoleClaimTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetRoleClaimValidator = new AddAspNetRoleClaimReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetRoleClaimTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetRole aspNetRole = null;
    
            int aspNetRoleClaimId = 0;
            try
            {
 
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var aspNetRoleClaimReqDto = new AddAspNetRoleClaimReqDto()
                {
                    RoleId = aspNetRole.Id,
                    ClaimType = "s",
                    ClaimValue = "s",

                };

                // Act
                var result = await Service.Save(aspNetRoleClaimReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetRoleClaimId = (int)result.Data.Id;
            }
            finally 
            {
                var aspNetRoleClaim = applicationDbContext.AspNetRoleClaims.Find(aspNetRoleClaimId);
                if (aspNetRoleClaim != null)
                {
                     await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetRoleClaimTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetRoleClaimReqDto  = new AddAspNetRoleClaimReqDto()
            {
                RoleId = invalidValue,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetRoleClaimValidator.ValidateAsync(aspNetRoleClaimReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

