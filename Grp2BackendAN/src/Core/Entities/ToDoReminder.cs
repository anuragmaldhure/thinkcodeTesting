


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class ToDoReminder
    {
        
        public int ReminderId { get; set; }
        
        public int ToDoTaskId { get; set; }
        
        public string SetById { get; set; }
        
        public DateTime SetForDateTime { get; set; }
        
        public DateTime? SentAtDateTime { get; set; }
        
        public bool IsActive { get; set; }
        public virtual ToDoMaster ToDoReminderToDoTaskIdfk { get; set; }
        public virtual AspNetUser ToDoReminderSetByIdfk { get; set; }
    }
}

