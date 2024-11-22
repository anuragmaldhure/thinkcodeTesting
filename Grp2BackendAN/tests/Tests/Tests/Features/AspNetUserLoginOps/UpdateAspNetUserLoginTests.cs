
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserLoginOps
{
    public class UpdateAspNetUserLoginTests : TestBaseCollection<IAspNetUserLoginService> 
    {
        private readonly IValidator<UpdateAspNetUserLoginReqDto> _updateAspNetUserLoginValidator;
        public UpdateAspNetUserLoginTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetUserLoginValidator = new UpdateAspNetUserLoginReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetUserLoginTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUserLogin = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetUserLoginReqDto  = new UpdateAspNetUserLoginReqDto()
                    {
                        LoginProvider = aspNetUserLogin.LoginProvider,
                        ProviderDisplayName = "string",
                        UserId = aspNetUser.Id,
                    };
                aspNetUser1 = aspNetUserLogin.AspNetUserLoginUserIdfk;
                // Act
                var updateResult = await Service.Update(updateAspNetUserLoginReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetUserLogin != null)
                {
                    await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateAspNetUserLoginTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin = null;
            try
            {
                aspNetUserLogin = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetUserLoginReqDto = new UpdateAspNetUserLoginReqDto()
                    {
                        LoginProvider = invalidValue,
                        ProviderDisplayName = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetUserLoginValidator.ValidateAsync(updateAspNetUserLoginReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetUserLogin != null)
                {
                    await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

