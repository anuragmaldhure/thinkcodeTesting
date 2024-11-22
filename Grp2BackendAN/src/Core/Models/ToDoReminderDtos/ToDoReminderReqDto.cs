
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    //Report 3 DTOs
    public class ReminderDto
    {
        public int ReminderId { get; set; }
        public string SetById { get; set; }
        public string SetByName { get; set; }
        public int ToDoTaskId { get; set; }
        public string TaskTitle { get; set; }
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public bool IsActive { get; set; }
    }

    public class DateDto
    {
        public DateTime Date { get; set; }
    }




    public class GetAllToDoReminderReqDto : BasePagination
    {     
     
        public FilterExpression<int>? ReminderId { get; set; } 
     
        public FilterExpression<int>? ToDoTaskId { get; set; } 
     
        public FilterExpression<string>? SetById { get; set; } 
     
        public FilterExpression<DateTime>? SetForDateTime { get; set; } 
     
        public FilterExpression<DateTime?>? SentAtDateTime { get; set; } 
     
        public FilterExpression<bool>? IsActive { get; set; } 
     }

    public class AddToDoReminderReqDto : IMapTo<ToDoReminder>
    {
        
        public int ToDoTaskId { get; set; }
        
        public string SetById { get; set; }
        
        public DateTime SetForDateTime { get; set; }
        
        public DateTime? SentAtDateTime { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class UpdateToDoReminderReqDto :AddToDoReminderReqDto, IMapTo<ToDoReminder>
    {
        public int ReminderId { get; set; }
    }

}

