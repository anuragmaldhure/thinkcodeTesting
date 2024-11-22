
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserTokenOps
{
    public class SaveAspNetUserTokenTests : TestBaseCollection<IAspNetUserTokenService> 
    {
        private readonly IValidator<AddAspNetUserTokenReqDto> _addAspNetUserTokenValidator;
        public SaveAspNetUserTokenTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetUserTokenValidator = new AddAspNetUserTokenReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetUserTokenTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
    
            string aspNetUserTokenUserId = null;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var aspNetUserTokenReqDto = new AddAspNetUserTokenReqDto()
                {
                    UserId = aspNetUser.Id,
                    LoginProvider = "s",
                    Name = "s",
                    Value = "s",

                };

                // Act
                var result = await Service.Save(aspNetUserTokenReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetUserTokenUserId = (string)result.Data.UserId;
            }
            finally 
            {
                var aspNetUserToken = applicationDbContext.AspNetUserTokens.Find(aspNetUserTokenUserId);
                if (aspNetUserToken != null)
                {
                     await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetUserTokenTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetUserTokenReqDto  = new AddAspNetUserTokenReqDto()
            {
                UserId = invalidValue,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetUserTokenValidator.ValidateAsync(aspNetUserTokenReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

