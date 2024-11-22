


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class ToDoChecklist
    {
        
        public int ToDoChecklistId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public bool IsComplete { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public int ToDoTaskId { get; set; }
        
        public bool IsDeleted { get; set; }
        public virtual ToDoMaster ToDoChecklistToDoTaskIdfk { get; set; }
    }
}

