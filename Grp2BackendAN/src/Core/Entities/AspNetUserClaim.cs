


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetUserClaim
    {
        
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
        public virtual AspNetUser AspNetUserClaimUserIdfk { get; set; }
    }
}

