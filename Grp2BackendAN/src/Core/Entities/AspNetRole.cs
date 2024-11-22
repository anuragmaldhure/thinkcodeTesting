


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetRole
    {
        
        public string Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? NormalizedName { get; set; }
        
        public string? ConcurrencyStamp { get; set; }
        public virtual List<AspNetRoleClaim> AspNetRoleClaimRoles { get; set; }
        public virtual List<AspNetUserRole> AspNetUserRoleRoles { get; set; }
    }
}

