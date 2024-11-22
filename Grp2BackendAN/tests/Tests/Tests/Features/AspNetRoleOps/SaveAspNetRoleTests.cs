
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleOps
{
    public class SaveAspNetRoleTests : TestBaseCollection<IAspNetRoleService> 
    {
        private readonly IValidator<AddAspNetRoleReqDto> _addAspNetRoleValidator;
        public SaveAspNetRoleTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetRoleValidator = new AddAspNetRoleReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetRoleTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
    
            string aspNetRoleId = null;
            try
            {
                var aspNetRoleReqDto = new AddAspNetRoleReqDto()
                {
                    Id = "s",
                    Name = "s",
                    NormalizedName = "s",
                    ConcurrencyStamp = "s",

                };

                // Act
                var result = await Service.Save(aspNetRoleReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetRoleId = (string)result.Data.Id;
            }
            finally 
            {
                var aspNetRole = applicationDbContext.AspNetRoles.Find(aspNetRoleId);
                if (aspNetRole != null)
                {
                     await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetRoleTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetRoleReqDto  = new AddAspNetRoleReqDto()
            {
                Id = invalidValue,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetRoleValidator.ValidateAsync(aspNetRoleReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

