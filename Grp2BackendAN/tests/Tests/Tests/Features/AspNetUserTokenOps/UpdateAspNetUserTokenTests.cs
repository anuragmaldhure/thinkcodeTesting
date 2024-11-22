
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserTokenOps
{
    public class UpdateAspNetUserTokenTests : TestBaseCollection<IAspNetUserTokenService> 
    {
        private readonly IValidator<UpdateAspNetUserTokenReqDto> _updateAspNetUserTokenValidator;
        public UpdateAspNetUserTokenTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetUserTokenValidator = new UpdateAspNetUserTokenReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetUserTokenTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetUserToken aspNetUserToken = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUserToken = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetUserTokenReqDto  = new UpdateAspNetUserTokenReqDto()
                    {
                        UserId = aspNetUserToken.UserId,
                        Value = "string",
                    };
                aspNetUser1 = aspNetUserToken.AspNetUserTokenUserIdfk;
                // Act
                var updateResult = await Service.Update(updateAspNetUserTokenReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetUserToken != null)
                {
                    await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateAspNetUserTokenTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetUserToken aspNetUserToken = null;
            try
            {
                aspNetUserToken = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetUserTokenReqDto = new UpdateAspNetUserTokenReqDto()
                    {
                        UserId = invalidValue,
                        Value = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetUserTokenValidator.ValidateAsync(updateAspNetUserTokenReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetUserToken != null)
                {
                    await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

