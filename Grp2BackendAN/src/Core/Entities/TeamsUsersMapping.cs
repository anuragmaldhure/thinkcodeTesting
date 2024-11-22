


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class TeamsUsersMapping
    {
        
        public int TeamId { get; set; }
        
        public string UserId { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime AddedAt { get; set; }
        
        public string AddedById { get; set; }
        public virtual Team TeamsUsersMappingTeamIdfk { get; set; }
        public virtual AspNetUser TeamsUsersMappingUserIdfk { get; set; }
        public virtual AspNetUser TeamsUsersMappingAddedByIdfk { get; set; }
    }
}

