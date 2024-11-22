
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserLoginOps
{
    public class SaveAspNetUserLoginTests : TestBaseCollection<IAspNetUserLoginService> 
    {
        private readonly IValidator<AddAspNetUserLoginReqDto> _addAspNetUserLoginValidator;
        public SaveAspNetUserLoginTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetUserLoginValidator = new AddAspNetUserLoginReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetUserLoginTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
    
            string aspNetUserLoginLoginProvider = null;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var aspNetUserLoginReqDto = new AddAspNetUserLoginReqDto()
                {
                    LoginProvider = "s",
                    ProviderKey = "s",
                    ProviderDisplayName = "s",
                    UserId = aspNetUser.Id,

                };

                // Act
                var result = await Service.Save(aspNetUserLoginReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetUserLoginLoginProvider = (string)result.Data.LoginProvider;
            }
            finally 
            {
                var aspNetUserLogin = applicationDbContext.AspNetUserLogins.Find(aspNetUserLoginLoginProvider);
                if (aspNetUserLogin != null)
                {
                     await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetUserLoginTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetUserLoginReqDto  = new AddAspNetUserLoginReqDto()
            {
                LoginProvider = invalidValue,
                UserId = "string",
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetUserLoginValidator.ValidateAsync(aspNetUserLoginReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

