


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddTeamReqDto
    public class AddTeamReqDtoValidator : AbstractValidator<AddTeamReqDto>
    {
        public AddTeamReqDtoValidator()
        {
         
            RuleFor(x => x.TeamName).NotEmpty().WithMessage("TeamName is required.");
            RuleFor(x => x.TeamName).MaximumLength(255).WithMessage("TeamName cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedAt).NotNull().WithMessage("CreatedAt is required.");
            RuleFor(x => x.CreatedById).NotEmpty().WithMessage("CreatedById is required.");
            RuleFor(x => x.CreatedById).MaximumLength(450).WithMessage("CreatedById cannot be longer than 450 characters.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
        }
    }

     // Validation class for updateTeamReqDto
    public class UpdateTeamReqDtoValidator : AbstractValidator<UpdateTeamReqDto>
    {
        public UpdateTeamReqDtoValidator()
        {           
            RuleFor(x => x.TeamId).NotNull().WithMessage("TeamId is required.");
            RuleFor(x => x.TeamName).NotEmpty().WithMessage("TeamName is required.");
            RuleFor(x => x.TeamName).MaximumLength(255).WithMessage("TeamName cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedAt).NotNull().WithMessage("CreatedAt is required.");
            RuleFor(x => x.CreatedById).NotEmpty().WithMessage("CreatedById is required.");
            RuleFor(x => x.CreatedById).MaximumLength(450).WithMessage("CreatedById cannot be longer than 450 characters.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
        }
    }
}


