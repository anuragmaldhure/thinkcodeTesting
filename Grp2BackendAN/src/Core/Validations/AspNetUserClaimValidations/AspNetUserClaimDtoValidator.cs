


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddAspNetUserClaimReqDto
    public class AddAspNetUserClaimReqDtoValidator : AbstractValidator<AddAspNetUserClaimReqDto>
    {
        public AddAspNetUserClaimReqDtoValidator()
        {
         
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.UserId).MaximumLength(450).WithMessage("UserId cannot be longer than 450 characters.");
            RuleFor(x => x.ClaimType).MaximumLength(255).WithMessage("ClaimType cannot be longer than 255 characters.");
            RuleFor(x => x.ClaimValue).MaximumLength(255).WithMessage("ClaimValue cannot be longer than 255 characters.");
        }
    }

     // Validation class for updateAspNetUserClaimReqDto
    public class UpdateAspNetUserClaimReqDtoValidator : AbstractValidator<UpdateAspNetUserClaimReqDto>
    {
        public UpdateAspNetUserClaimReqDtoValidator()
        {           
            RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.UserId).MaximumLength(450).WithMessage("UserId cannot be longer than 450 characters.");
            RuleFor(x => x.ClaimType).MaximumLength(255).WithMessage("ClaimType cannot be longer than 255 characters.");
            RuleFor(x => x.ClaimValue).MaximumLength(255).WithMessage("ClaimValue cannot be longer than 255 characters.");
        }
    }
}


