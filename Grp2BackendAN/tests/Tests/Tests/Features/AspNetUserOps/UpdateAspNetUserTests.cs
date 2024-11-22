
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserOps
{
    public class UpdateAspNetUserTests : TestBaseCollection<IAspNetUserService> 
    {
        private readonly IValidator<UpdateAspNetUserReqDto> _updateAspNetUserValidator;
        public UpdateAspNetUserTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _updateAspNetUserValidator = new UpdateAspNetUserReqDtoValidator();
        }

        [Fact]
        public async Task UpdateAspNetUserTest_ReturnsSuccessResult_WhenGivenValidInputs()
        {
            // Arrange
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                var updateAspNetUserReqDto  = new UpdateAspNetUserReqDto()
                    {
                        Id = aspNetUser.Id,
                        FirstName = "string",
                        LastName = "string",
                        IsActive = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        UserName = "string",
                        NormalizedUserName = "string",
                        Email = "string",
                        NormalizedEmail = "string",
                        EmailConfirmed = false,
                        PasswordHash = "string",
                        SecurityStamp = "string",
                        ConcurrencyStamp = "string",
                        PhoneNumber = "string",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = DateTimeOffset.Now,
                        LockoutEnabled = false,
                        AccessFailedCount = 1,
                    };
                // Act
                var updateResult = await Service.Update(updateAspNetUserReqDto);

                // Assert
                updateResult.Status.Should().Be(HttpStatusCode.OK);
            }
            finally 
            {
                if (aspNetUser != null)
                {
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
                  await applicationDbContext.SaveChangesAsync();
				
                }
            }
        }


        [Fact]
        public async Task UpdateAspNetUserTest_ReturnsFailureResult_ForInvalidValueInField()
        {
            // Arrange
            AspNetUser aspNetUser = null;
            try
            {
                aspNetUser = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                string invalidValue = new string('A', 450 + 1);
                var  updateAspNetUserReqDto = new UpdateAspNetUserReqDto()
                    {
                        Id = invalidValue,
                        FirstName = "string",
                    };

                // Act & Assert : validate the requestDto 
                var validationResult = await _updateAspNetUserValidator.ValidateAsync(updateAspNetUserReqDto);
                validationResult.IsValid.Should().BeFalse();
            }
            finally 
            {
                if (aspNetUser != null)
                {
                    await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }


    }
}

