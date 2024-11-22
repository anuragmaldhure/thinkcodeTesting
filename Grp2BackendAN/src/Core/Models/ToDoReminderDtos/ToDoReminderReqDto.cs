
namespace thinkbridge.Grp2BackendAN.Core.Models
{

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

