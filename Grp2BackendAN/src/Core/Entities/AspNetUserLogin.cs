


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetUserLogin
    {
        
        public string LoginProvider { get; set; }
        
        public string ProviderKey { get; set; }
        
        public string? ProviderDisplayName { get; set; }
        
        public string UserId { get; set; }
        public virtual AspNetUser AspNetUserLoginUserIdfk { get; set; }
    }
}

