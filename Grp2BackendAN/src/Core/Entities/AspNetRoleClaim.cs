


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetRoleClaim
    {
        
        public int Id { get; set; }
        
        public string RoleId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
        public virtual AspNetRole AspNetRoleClaimRoleIdfk { get; set; }
    }
}

