


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddToDoReminderReqDto
    public class AddToDoReminderReqDtoValidator : AbstractValidator<AddToDoReminderReqDto>
    {
        public AddToDoReminderReqDtoValidator()
        {
         
            RuleFor(x => x.ToDoTaskId).NotNull().WithMessage("ToDoTaskId is required.");
            RuleFor(x => x.SetById).NotEmpty().WithMessage("SetById is required.");
            RuleFor(x => x.SetById).MaximumLength(450).WithMessage("SetById cannot be longer than 450 characters.");
            RuleFor(x => x.SetForDateTime).NotNull().WithMessage("SetForDateTime is required.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
        }
    }

     // Validation class for updateToDoReminderReqDto
    public class UpdateToDoReminderReqDtoValidator : AbstractValidator<UpdateToDoReminderReqDto>
    {
        public UpdateToDoReminderReqDtoValidator()
        {           
            RuleFor(x => x.ReminderId).NotNull().WithMessage("ReminderId is required.");
            RuleFor(x => x.ToDoTaskId).NotNull().WithMessage("ToDoTaskId is required.");
            RuleFor(x => x.SetById).NotEmpty().WithMessage("SetById is required.");
            RuleFor(x => x.SetById).MaximumLength(450).WithMessage("SetById cannot be longer than 450 characters.");
            RuleFor(x => x.SetForDateTime).NotNull().WithMessage("SetForDateTime is required.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
        }
    }
}


