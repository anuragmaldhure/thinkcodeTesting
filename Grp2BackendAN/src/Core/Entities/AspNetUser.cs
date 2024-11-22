


namespace thinkbridge.Grp2BackendAN.Core.Entities
{
    public class AspNetUser
    {
        
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
        
        public string? UserName { get; set; }
        
        public string? NormalizedUserName { get; set; }
        
        public string? Email { get; set; }
        
        public string? NormalizedEmail { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public string? PasswordHash { get; set; }
        
        public string? SecurityStamp { get; set; }
        
        public string? ConcurrencyStamp { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }
        
        public bool TwoFactorEnabled { get; set; }
        
        public DateTimeOffset? LockoutEnd { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public int AccessFailedCount { get; set; }
        public virtual List<AspNetUserClaim> AspNetUserClaimUsers { get; set; }
        public virtual List<AspNetUserLogin> AspNetUserLoginUsers { get; set; }
        public virtual List<AspNetUserRole> AspNetUserRoleUsers { get; set; }
        public virtual List<AspNetUserToken> AspNetUserTokenUsers { get; set; }
        public virtual List<Team> TeamCreatedBies { get; set; }
        public virtual List<TeamsUsersMapping> TeamsUsersMappingUsers { get; set; }
        public virtual List<TeamsUsersMapping> TeamsUsersMappingAddedBies { get; set; }
        public virtual List<ToDoMaster> ToDoMasterCreatedBies { get; set; }
        public virtual List<ToDoReminder> ToDoReminderSetBies { get; set; }
    }
}

