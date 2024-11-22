


namespace thinkbridge.Grp2BackendAN.Core.Validations
{
    // Validation class for AddToDoMasterReqDto
    public class AddToDoMasterReqDtoValidator : AbstractValidator<AddToDoMasterReqDto>
    {
        public AddToDoMasterReqDtoValidator()
        {
         
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.IsComplete).NotNull().WithMessage("IsComplete is required.");
            RuleFor(x => x.RepeatFrequency).NotEmpty().WithMessage("RepeatFrequency is required.");
            RuleFor(x => x.RepeatFrequency).MaximumLength(255).WithMessage("RepeatFrequency cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.CreatedById).NotEmpty().WithMessage("CreatedById is required.");
            RuleFor(x => x.CreatedById).MaximumLength(450).WithMessage("CreatedById cannot be longer than 450 characters.");
            RuleFor(x => x.AssignedToId).NotEmpty().WithMessage("AssignedToId is required.");
            RuleFor(x => x.AssignedToId).MaximumLength(450).WithMessage("AssignedToId cannot be longer than 450 characters.");
            RuleFor(x => x.IsAssignedToTeam).NotNull().WithMessage("IsAssignedToTeam is required.");
            RuleFor(x => x.HasChecklist).NotNull().WithMessage("HasChecklist is required.");
            RuleFor(x => x.HasReminder).NotNull().WithMessage("HasReminder is required.");
            RuleFor(x => x.PercentageCompleted).NotNull().WithMessage("PercentageCompleted is required.");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("IsDeleted is required.");
            RuleFor(x => x.IsStarred).NotNull().WithMessage("IsStarred is required.");
            RuleFor(x => x.MediaAttachmentType).NotEmpty().WithMessage("MediaAttachmentType is required.");
            RuleFor(x => x.MediaAttachmentType).MaximumLength(255).WithMessage("MediaAttachmentType cannot be longer than 255 characters.");
            RuleFor(x => x.MediaAttachmentURL).NotEmpty().WithMessage("MediaAttachmentURL is required.");
            RuleFor(x => x.MediaAttachmentURL).MaximumLength(255).WithMessage("MediaAttachmentURL cannot be longer than 255 characters.");
        }
    }

     // Validation class for updateToDoMasterReqDto
    public class UpdateToDoMasterReqDtoValidator : AbstractValidator<UpdateToDoMasterReqDto>
    {
        public UpdateToDoMasterReqDtoValidator()
        {           
            RuleFor(x => x.ToDoTaskId).NotNull().WithMessage("ToDoTaskId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.IsComplete).NotNull().WithMessage("IsComplete is required.");
            RuleFor(x => x.RepeatFrequency).NotEmpty().WithMessage("RepeatFrequency is required.");
            RuleFor(x => x.RepeatFrequency).MaximumLength(255).WithMessage("RepeatFrequency cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedDate).NotNull().WithMessage("CreatedDate is required.");
            RuleFor(x => x.CreatedById).NotEmpty().WithMessage("CreatedById is required.");
            RuleFor(x => x.CreatedById).MaximumLength(450).WithMessage("CreatedById cannot be longer than 450 characters.");
            RuleFor(x => x.AssignedToId).NotEmpty().WithMessage("AssignedToId is required.");
            RuleFor(x => x.AssignedToId).MaximumLength(450).WithMessage("AssignedToId cannot be longer than 450 characters.");
            RuleFor(x => x.IsAssignedToTeam).NotNull().WithMessage("IsAssignedToTeam is required.");
            RuleFor(x => x.HasChecklist).NotNull().WithMessage("HasChecklist is required.");
            RuleFor(x => x.HasReminder).NotNull().WithMessage("HasReminder is required.");
            RuleFor(x => x.PercentageCompleted).NotNull().WithMessage("PercentageCompleted is required.");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("IsDeleted is required.");
            RuleFor(x => x.IsStarred).NotNull().WithMessage("IsStarred is required.");
            RuleFor(x => x.MediaAttachmentType).NotEmpty().WithMessage("MediaAttachmentType is required.");
            RuleFor(x => x.MediaAttachmentType).MaximumLength(255).WithMessage("MediaAttachmentType cannot be longer than 255 characters.");
            RuleFor(x => x.MediaAttachmentURL).NotEmpty().WithMessage("MediaAttachmentURL is required.");
            RuleFor(x => x.MediaAttachmentURL).MaximumLength(255).WithMessage("MediaAttachmentURL cannot be longer than 255 characters.");
        }
    }
}


