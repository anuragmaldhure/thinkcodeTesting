


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddAspNetUserReqDto
    public class AddAspNetUserReqDtoValidator : AbstractValidator<AddAspNetUserReqDto>
    {
        public AddAspNetUserReqDtoValidator()
        {
         
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Id).MaximumLength(450).WithMessage("Id cannot be longer than 450 characters.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.FirstName).MaximumLength(255).WithMessage("FirstName cannot be longer than 255 characters.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.LastName).MaximumLength(255).WithMessage("LastName cannot be longer than 255 characters.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.UpdatedDate).NotNull().WithMessage("UpdatedDate is required.");
            RuleFor(x => x.UserName).MaximumLength(256).WithMessage("UserName cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedUserName).MaximumLength(256).WithMessage("NormalizedUserName cannot be longer than 256 characters.");
            RuleFor(x => x.Email).MaximumLength(256).WithMessage("Email cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedEmail).MaximumLength(256).WithMessage("NormalizedEmail cannot be longer than 256 characters.");
            RuleFor(x => x.EmailConfirmed).NotNull().WithMessage("EmailConfirmed is required.");
            RuleFor(x => x.PasswordHash).MaximumLength(255).WithMessage("PasswordHash cannot be longer than 255 characters.");
            RuleFor(x => x.SecurityStamp).MaximumLength(255).WithMessage("SecurityStamp cannot be longer than 255 characters.");
            RuleFor(x => x.ConcurrencyStamp).MaximumLength(255).WithMessage("ConcurrencyStamp cannot be longer than 255 characters.");
            RuleFor(x => x.PhoneNumber).MaximumLength(255).WithMessage("PhoneNumber cannot be longer than 255 characters.");
            RuleFor(x => x.PhoneNumberConfirmed).NotNull().WithMessage("PhoneNumberConfirmed is required.");
            RuleFor(x => x.TwoFactorEnabled).NotNull().WithMessage("TwoFactorEnabled is required.");
            RuleFor(x => x.LockoutEnabled).NotNull().WithMessage("LockoutEnabled is required.");
            RuleFor(x => x.AccessFailedCount).NotNull().WithMessage("AccessFailedCount is required.");
        }
    }

     // Validation class for updateAspNetUserReqDto
    public class UpdateAspNetUserReqDtoValidator : AbstractValidator<UpdateAspNetUserReqDto>
    {
        public UpdateAspNetUserReqDtoValidator()
        {           
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Id).MaximumLength(450).WithMessage("Id cannot be longer than 450 characters.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.FirstName).MaximumLength(255).WithMessage("FirstName cannot be longer than 255 characters.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.LastName).MaximumLength(255).WithMessage("LastName cannot be longer than 255 characters.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.UpdatedDate).NotNull().WithMessage("UpdatedDate is required.");
            RuleFor(x => x.UserName).MaximumLength(256).WithMessage("UserName cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedUserName).MaximumLength(256).WithMessage("NormalizedUserName cannot be longer than 256 characters.");
            RuleFor(x => x.Email).MaximumLength(256).WithMessage("Email cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedEmail).MaximumLength(256).WithMessage("NormalizedEmail cannot be longer than 256 characters.");
            RuleFor(x => x.EmailConfirmed).NotNull().WithMessage("EmailConfirmed is required.");
            RuleFor(x => x.PasswordHash).MaximumLength(255).WithMessage("PasswordHash cannot be longer than 255 characters.");
            RuleFor(x => x.SecurityStamp).MaximumLength(255).WithMessage("SecurityStamp cannot be longer than 255 characters.");
            RuleFor(x => x.ConcurrencyStamp).MaximumLength(255).WithMessage("ConcurrencyStamp cannot be longer than 255 characters.");
            RuleFor(x => x.PhoneNumber).MaximumLength(255).WithMessage("PhoneNumber cannot be longer than 255 characters.");
            RuleFor(x => x.PhoneNumberConfirmed).NotNull().WithMessage("PhoneNumberConfirmed is required.");
            RuleFor(x => x.TwoFactorEnabled).NotNull().WithMessage("TwoFactorEnabled is required.");
            RuleFor(x => x.LockoutEnabled).NotNull().WithMessage("LockoutEnabled is required.");
            RuleFor(x => x.AccessFailedCount).NotNull().WithMessage("AccessFailedCount is required.");
        }
    }
}


