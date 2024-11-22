
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllTeamReqDto : BasePagination
    {     
     
        public FilterExpression<int>? TeamId { get; set; } 
     
        public FilterExpression<string>? TeamName { get; set; } 
     
        public FilterExpression<string>? Description { get; set; } 
     
        public FilterExpression<DateTime>? CreatedAt { get; set; } 
     
        public FilterExpression<string>? CreatedById { get; set; } 
     
        public FilterExpression<bool>? IsActive { get; set; } 
     }

    public class AddTeamReqDto : IMapTo<Team>
    {
        
        public string TeamName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string CreatedById { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class UpdateTeamReqDto :AddTeamReqDto, IMapTo<Team>
    {
        public int TeamId { get; set; }
    }

}

