
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllTeamsUsersMappingReqDto : BasePagination
    {     
     
        public FilterExpression<int>? TeamId { get; set; } 
     
        public FilterExpression<string>? UserId { get; set; } 
     
        public FilterExpression<bool>? IsActive { get; set; } 
     
        public FilterExpression<DateTime>? AddedAt { get; set; } 
     
        public FilterExpression<string>? AddedById { get; set; } 
     }

    public class AddTeamsUsersMappingReqDto : IMapTo<TeamsUsersMapping>
    {
        
        public string UserId { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime AddedAt { get; set; }
        
        public string AddedById { get; set; }
    }

    public class UpdateTeamsUsersMappingReqDto :AddTeamsUsersMappingReqDto, IMapTo<TeamsUsersMapping>
    {
        public int TeamId { get; set; }
    }

}

