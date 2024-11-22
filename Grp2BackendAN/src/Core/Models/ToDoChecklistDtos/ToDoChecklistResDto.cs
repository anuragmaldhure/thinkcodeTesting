
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class ToDoChecklistResDto:  IMapFrom<ToDoChecklist>
    {     
        
        public int ToDoChecklistId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public bool IsComplete { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public int ToDoTaskId { get; set; }
        
        public bool IsDeleted { get; set; }
    }

   public class ToDoChecklistResDetailDto: ToDoChecklistResDto, IMapFrom<ToDoChecklist>
   {
   
        public virtual ToDoMasterResDto? ToDoChecklistToDoTaskIdfk { get; set; }
   }


}

