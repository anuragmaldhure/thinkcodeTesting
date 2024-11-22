


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddToDoChecklistReqDto
    public class AddToDoChecklistReqDtoValidator : AbstractValidator<AddToDoChecklistReqDto>
    {
        public AddToDoChecklistReqDtoValidator()
        {
         
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.IsComplete).NotNull().WithMessage("IsComplete is required.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.ToDoTaskId).NotNull().WithMessage("ToDoTaskId is required.");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("IsDeleted is required.");
        }
    }

     // Validation class for updateToDoChecklistReqDto
    public class UpdateToDoChecklistReqDtoValidator : AbstractValidator<UpdateToDoChecklistReqDto>
    {
        public UpdateToDoChecklistReqDtoValidator()
        {           
            RuleFor(x => x.ToDoChecklistId).NotNull().WithMessage("ToDoChecklistId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.IsComplete).NotNull().WithMessage("IsComplete is required.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.ToDoTaskId).NotNull().WithMessage("ToDoTaskId is required.");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("IsDeleted is required.");
        }
    }
}


