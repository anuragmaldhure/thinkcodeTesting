


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class Team
    {
        
        public int TeamId { get; set; }
        
        public string TeamName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string CreatedById { get; set; }
        
        public bool IsActive { get; set; }
        public virtual List<TeamsUsersMapping> TeamsUsersMappingTeams { get; set; }
        public virtual AspNetUser TeamCreatedByIdfk { get; set; }
    }
}

