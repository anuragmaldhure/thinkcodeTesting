
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class ToDoMasterResDto:  IMapFrom<ToDoMaster>
    {     
        
        public int ToDoTaskId { get; set; }
        
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

   public class ToDoMasterResDetailDto: ToDoMasterResDto, IMapFrom<ToDoMaster>
   {
        public virtual List<ToDoChecklistResDto>? ToDoChecklistToDoTasks { get; set; }
     public virtual List<ToDoReminderResDto>? ToDoReminderToDoTasks { get; set; }

        public virtual AspNetUserResDto? ToDoMasterCreatedByIdfk { get; set; }
   }


}

