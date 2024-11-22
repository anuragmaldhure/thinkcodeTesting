
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class TeamResDto:  IMapFrom<Team>
    {     
        
        public int TeamId { get; set; }
        
        public string TeamName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string CreatedById { get; set; }
        
        public bool IsActive { get; set; }
    }

   public class TeamResDetailDto: TeamResDto, IMapFrom<Team>
   {
        public virtual List<TeamsUsersMappingResDto>? TeamsUsersMappingTeams { get; set; }

        public virtual AspNetUserResDto? TeamCreatedByIdfk { get; set; }
   }


}

