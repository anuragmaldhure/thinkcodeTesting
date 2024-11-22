


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetUserRole
    {
        
        public string UserId { get; set; }
        
        public string RoleId { get; set; }
        public virtual AspNetUser AspNetUserRoleUserIdfk { get; set; }
        public virtual AspNetRole AspNetUserRoleRoleIdfk { get; set; }
    }
}

