
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetUserResDto:  IMapFrom<AspNetUser>
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
    }

   public class AspNetUserResDetailDto: AspNetUserResDto, IMapFrom<AspNetUser>
   {
        public virtual List<AspNetUserClaimResDto>? AspNetUserClaimUsers { get; set; }
     public virtual List<AspNetUserLoginResDto>? AspNetUserLoginUsers { get; set; }
     public virtual List<AspNetUserRoleResDto>? AspNetUserRoleUsers { get; set; }
     public virtual List<AspNetUserTokenResDto>? AspNetUserTokenUsers { get; set; }
     public virtual List<TeamResDto>? TeamCreatedBies { get; set; }
     public virtual List<TeamsUsersMappingResDto>? TeamsUsersMappingUsers { get; set; }
     public virtual List<TeamsUsersMappingResDto>? TeamsUsersMappingAddedBies { get; set; }
     public virtual List<ToDoMasterResDto>? ToDoMasterCreatedBies { get; set; }
     public virtual List<ToDoReminderResDto>? ToDoReminderSetBies { get; set; }

   }


}

