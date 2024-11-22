


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddAspNetRoleReqDto
    public class AddAspNetRoleReqDtoValidator : AbstractValidator<AddAspNetRoleReqDto>
    {
        public AddAspNetRoleReqDtoValidator()
        {
         
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Id).MaximumLength(450).WithMessage("Id cannot be longer than 450 characters.");
            RuleFor(x => x.Name).MaximumLength(256).WithMessage("Name cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedName).MaximumLength(256).WithMessage("NormalizedName cannot be longer than 256 characters.");
            RuleFor(x => x.ConcurrencyStamp).MaximumLength(255).WithMessage("ConcurrencyStamp cannot be longer than 255 characters.");
        }
    }

     // Validation class for updateAspNetRoleReqDto
    public class UpdateAspNetRoleReqDtoValidator : AbstractValidator<UpdateAspNetRoleReqDto>
    {
        public UpdateAspNetRoleReqDtoValidator()
        {           
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Id).MaximumLength(450).WithMessage("Id cannot be longer than 450 characters.");
            RuleFor(x => x.Name).MaximumLength(256).WithMessage("Name cannot be longer than 256 characters.");
            RuleFor(x => x.NormalizedName).MaximumLength(256).WithMessage("NormalizedName cannot be longer than 256 characters.");
            RuleFor(x => x.ConcurrencyStamp).MaximumLength(255).WithMessage("ConcurrencyStamp cannot be longer than 255 characters.");
        }
    }
}


