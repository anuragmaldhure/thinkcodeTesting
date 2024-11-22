
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllToDoChecklistReqDto : BasePagination
    {     
     
        public FilterExpression<int>? ToDoChecklistId { get; set; } 
     
        public FilterExpression<string>? Title { get; set; } 
     
        public FilterExpression<string>? Description { get; set; } 
     
        public FilterExpression<bool>? IsComplete { get; set; } 
     
        public FilterExpression<DateTime>? CreatedDate { get; set; } 
     
        public FilterExpression<DateTime?>? UpdatedDate { get; set; } 
     
        public FilterExpression<int>? ToDoTaskId { get; set; } 
     
        public FilterExpression<bool>? IsDeleted { get; set; } 
     }

    public class AddToDoChecklistReqDto : IMapTo<ToDoChecklist>
    {
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public bool IsComplete { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public int ToDoTaskId { get; set; }
        
        public bool IsDeleted { get; set; }
    }

    public class UpdateToDoChecklistReqDto :AddToDoChecklistReqDto, IMapTo<ToDoChecklist>
    {
        public int ToDoChecklistId { get; set; }
    }

}

