
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class TeamsUsersMappingResDto:  IMapFrom<TeamsUsersMapping>
    {     
        
        public int TeamId { get; set; }
        
        public string UserId { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime AddedAt { get; set; }
        
        public string AddedById { get; set; }
    }

   public class TeamsUsersMappingResDetailDto: TeamsUsersMappingResDto, IMapFrom<TeamsUsersMapping>
   {
   
        public virtual TeamResDto? TeamsUsersMappingTeamIdfk { get; set; }
        public virtual AspNetUserResDto? TeamsUsersMappingUserIdfk { get; set; }
        public virtual AspNetUserResDto? TeamsUsersMappingAddedByIdfk { get; set; }
   }


}

