
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class ToDoReminderResDto:  IMapFrom<ToDoReminder>
    {     
        
        public int ReminderId { get; set; }
        
        public int ToDoTaskId { get; set; }
        
        public string SetById { get; set; }
        
        public DateTime SetForDateTime { get; set; }
        
        public DateTime? SentAtDateTime { get; set; }
        
        public bool IsActive { get; set; }
    }

   public class ToDoReminderResDetailDto: ToDoReminderResDto, IMapFrom<ToDoReminder>
   {
   
        public virtual ToDoMasterResDto? ToDoReminderToDoTaskIdfk { get; set; }
        public virtual AspNetUserResDto? ToDoReminderSetByIdfk { get; set; }
   }


}

