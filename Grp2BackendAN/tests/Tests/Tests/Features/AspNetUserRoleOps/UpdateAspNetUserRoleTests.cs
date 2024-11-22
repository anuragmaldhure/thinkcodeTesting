
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserRoleOps
{
    public class UpdateAspNetUserRoleTests : TestBaseCollection<IAspNetUserRoleService> 
    {
        private readonly IValidator<UpdateAspNetUserRoleReqDto> _updateAspNetUserRoleValidator;
        public UpdateAspNetUserRoleTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetUserRoleValidator = new UpdateAspNetUserRoleReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetUserRoleTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetUserRole aspNetUserRole = null;
            AspNetUser aspNetUser = null;
            AspNetUser aspNetUser1 = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUserRole = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetUserRoleReqDto  = new UpdateAspNetUserRoleReqDto()
                    {
                        UserId = aspNetUserRole.UserId,
                    };
                aspNetUser1 = aspNetUserRole.AspNetUserRoleUserIdfk;
                // Act
                var updateResult = await Service.Update(updateAspNetUserRoleReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetUserRole != null)
                {
                    await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
				
                }
                if (aspNetUser1 != null)
					await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                  await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task UpdateAspNetUserRoleTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetUserRole aspNetUserRole = null;
            try
            {
                aspNetUserRole = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetUserRoleReqDto = new UpdateAspNetUserRoleReqDto()
                    {
                        UserId = invalidValue,
                        RoleId = aspNetUserRole.RoleId,
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetUserRoleValidator.ValidateAsync(updateAspNetUserRoleReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetUserRole != null)
                {
                    await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

