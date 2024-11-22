


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddAspNetRoleClaimReqDto
    public class AddAspNetRoleClaimReqDtoValidator : AbstractValidator<AddAspNetRoleClaimReqDto>
    {
        public AddAspNetRoleClaimReqDtoValidator()
        {
         
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId is required.");
            RuleFor(x => x.RoleId).MaximumLength(450).WithMessage("RoleId cannot be longer than 450 characters.");
            RuleFor(x => x.ClaimType).MaximumLength(255).WithMessage("ClaimType cannot be longer than 255 characters.");
            RuleFor(x => x.ClaimValue).MaximumLength(255).WithMessage("ClaimValue cannot be longer than 255 characters.");
        }
    }

     // Validation class for updateAspNetRoleClaimReqDto
    public class UpdateAspNetRoleClaimReqDtoValidator : AbstractValidator<UpdateAspNetRoleClaimReqDto>
    {
        public UpdateAspNetRoleClaimReqDtoValidator()
        {           
            RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId is required.");
            RuleFor(x => x.RoleId).MaximumLength(450).WithMessage("RoleId cannot be longer than 450 characters.");
            RuleFor(x => x.ClaimType).MaximumLength(255).WithMessage("ClaimType cannot be longer than 255 characters.");
            RuleFor(x => x.ClaimValue).MaximumLength(255).WithMessage("ClaimValue cannot be longer than 255 characters.");
        }
    }
}


