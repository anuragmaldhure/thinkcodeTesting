
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserRoleOps
{
    public class SaveAspNetUserRoleTests : TestBaseCollection<IAspNetUserRoleService> 
    {
        private readonly IValidator<AddAspNetUserRoleReqDto> _addAspNetUserRoleValidator;
        public SaveAspNetUserRoleTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetUserRoleValidator = new AddAspNetUserRoleReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetUserRoleTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
   		
            AspNetUser aspNetUser = null;
   		
            AspNetRole aspNetRole = null;
    
            string aspNetUserRoleUserId = null;
            try
            {
 
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);	
 
                aspNetRole = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);	
                await applicationDbContext.SaveChangesAsync();
                var aspNetUserRoleReqDto = new AddAspNetUserRoleReqDto()
                {
                    UserId = aspNetUser.Id,
                    RoleId = aspNetRole.Id,

                };

                // Act
                var result = await Service.Save(aspNetUserRoleReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetUserRoleUserId = (string)result.Data.UserId;
            }
            finally 
            {
                var aspNetUserRole = applicationDbContext.AspNetUserRoles.Find(aspNetUserRoleUserId);
                if (aspNetUserRole != null)
                {
                     await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetUserRoleTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetUserRoleReqDto  = new AddAspNetUserRoleReqDto()
            {
                UserId = invalidValue,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetUserRoleValidator.ValidateAsync(aspNetUserRoleReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

