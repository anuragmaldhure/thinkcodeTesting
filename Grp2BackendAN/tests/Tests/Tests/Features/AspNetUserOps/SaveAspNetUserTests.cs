
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.Core.Validations;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserOps
{
    public class SaveAspNetUserTests : TestBaseCollection<IAspNetUserService> 
    {
        private readonly IValidator<AddAspNetUserReqDto> _addAspNetUserValidator;
        public SaveAspNetUserTests(SetupFixture setupFixture) : base(setupFixture)
        {
            _addAspNetUserValidator = new AddAspNetUserReqDtoValidator();
        }

        [Fact]
        public async Task SaveAspNetUserTest_ReturnsSuccessResult_WhenGivenMinInput()
        {
            // Arrange 
    
            string aspNetUserId = null;
            try
            {
                var aspNetUserReqDto = new AddAspNetUserReqDto()
                {
                    Id = "s",
                    FirstName = "s",
                    LastName = "s",
                    IsActive = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserName = "s",
                    NormalizedUserName = "s",
                    Email = "s",
                    NormalizedEmail = "s",
                    EmailConfirmed = false,
                    PasswordHash = "s",
                    SecurityStamp = "s",
                    ConcurrencyStamp = "s",
                    PhoneNumber = "s",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = DateTimeOffset.Now,
                    LockoutEnabled = false,
                    AccessFailedCount = 1,

                };

                // Act
                var result = await Service.Save(aspNetUserReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                aspNetUserId = (string)result.Data.Id;
            }
            finally 
            {
                var aspNetUser = applicationDbContext.AspNetUsers.Find(aspNetUserId);
                if (aspNetUser != null)
                {
                     await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }


        [Fact]
        public async Task SaveAspNetUserTest_ReturnsFailureResult_ForValidationException()
        {
            // Arrange
            string invalidValue = new string('A', 450 + 1);
            var aspNetUserReqDto  = new AddAspNetUserReqDto()
            {
                Id = invalidValue,
                FirstName = "string",
                LastName = "string",
                IsActive = default,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                EmailConfirmed = default,
                PhoneNumberConfirmed = default,
                TwoFactorEnabled = default,
                LockoutEnabled = default,
                AccessFailedCount = 1,
            };

            // Act & Assert : validate the requestDto 
            var validationResult = await _addAspNetUserValidator.ValidateAsync(aspNetUserReqDto);
            validationResult.IsValid.Should().BeFalse();
        }

    }
}

