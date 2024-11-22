
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleOps
{
    public class UpdateAspNetRoleTests : TestBaseCollection<IAspNetRoleService> 
    {
        private readonly IValidator<UpdateAspNetRoleReqDto> _updateAspNetRoleValidator;
        public UpdateAspNetRoleTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetRoleValidator = new UpdateAspNetRoleReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetRoleTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetRole aspNetRole = null;
            try
            {
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetRoleReqDto  = new UpdateAspNetRoleReqDto()
                    {
                        Id = aspNetRole.Id,
                        Name = "string",
                        NormalizedName = "string",
                        ConcurrencyStamp = "string",
                    };
                // Act
                var updateResult = await Service.Update(updateAspNetRoleReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetRole != null)
                {
                    await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
                  await applicationDbContext.SaveChangesAsync();
				
                }
            }
        }


        [Fact]
        public async Task UpdateAspNetRoleTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetRole aspNetRole = null;
            try
            {
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetRoleReqDto = new UpdateAspNetRoleReqDto()
                    {
                        Id = invalidValue,
                        Name = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetRoleValidator.ValidateAsync(updateAspNetRoleReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetRole != null)
                {
                    await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

