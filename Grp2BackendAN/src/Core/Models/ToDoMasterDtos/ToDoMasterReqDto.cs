
namespace thinkbridge.Grp2BackendAN.Core.Models
{
    //Added DTO
    public class MarkTaskCompletedDTO
    {
        public int ToDoTaskId { get; set; }
        public bool IsComplete { get; set; }
    }

    //Added DTO
    public class MarkSingleChecklistCompletedDTO
    {
        /// <summary>
        /// Gets or sets the ID of the ToDo checklist.
        /// </summary>
        public int ToDoChecklistId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the checklist is complete.
        /// </summary>
        public bool IsComplete { get; set; }
    }

    //Added DTO for reassigning task
    public class UpdateToDoMasterDTO
    {
        public string AssignedToId { get; set; }
        public bool IsAssignedToTeam { get; set; }
    }

    //Report 1 
    public class ToDoMasterDto
    {
        public int ToDoTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedToId { get; set; }
    }

    public class DateRangeDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }



    public class GetAllToDoMasterReqDto : BasePagination
    {     
     
        public FilterExpression<int>? ToDoTaskId { get; set; } 
     
        public FilterExpression<string>? Title { get; set; } 
     
        public FilterExpression<string>? Description { get; set; } 
     
        public FilterExpression<bool>? IsComplete { get; set; } 
     
        public FilterExpression<string>? RepeatFrequency { get; set; } 
     
        public FilterExpression<DateTime>? CreatedDate { get; set; } 
     
        public FilterExpression<DateTime?>? DueDate { get; set; } 
     
        public FilterExpression<DateTime?>? UpdatedDate { get; set; } 
     
        public FilterExpression<DateTime?>? CompletedDate { get; set; } 
     
        public FilterExpression<string>? CreatedById { get; set; } 
     
        public FilterExpression<string>? AssignedToId { get; set; } 
     
        public FilterExpression<bool>? IsAssignedToTeam { get; set; } 
     
        public FilterExpression<bool>? HasChecklist { get; set; } 
     
        public FilterExpression<bool>? HasReminder { get; set; } 
     
        public FilterExpression<decimal>? PercentageCompleted { get; set; } 
     
        public FilterExpression<bool>? IsDeleted { get; set; } 
     
        public FilterExpression<bool>? IsStarred { get; set; } 
     
        public FilterExpression<string>? MediaAttachmentType { get; set; } 
     
        public FilterExpression<string>? MediaAttachmentURL { get; set; } 
     }

    public class AddToDoMasterReqDto : IMapTo<ToDoMaster>
    {
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public bool IsComplete { get; set; }
        
        public string RepeatFrequency { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        public string CreatedById { get; set; }
        
        public string AssignedToId { get; set; }
        
        public bool IsAssignedToTeam { get; set; }
        
        public bool HasChecklist { get; set; }
        
        public bool HasReminder { get; set; }
        
        public decimal PercentageCompleted { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public bool IsStarred { get; set; }
        
        public string MediaAttachmentType { get; set; }
        
        public string MediaAttachmentURL { get; set; }
    }

    public class UpdateToDoMasterReqDto :AddToDoMasterReqDto, IMapTo<ToDoMaster>
    {
        public int ToDoTaskId { get; set; }
    }

}

