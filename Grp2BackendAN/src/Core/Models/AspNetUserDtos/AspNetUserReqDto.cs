
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetUserReqDto : BasePagination
    {     
     
        public FilterExpression<string>? Id { get; set; } 
     
        public FilterExpression<string>? FirstName { get; set; } 
     
        public FilterExpression<string>? LastName { get; set; } 
     
        public FilterExpression<bool>? IsActive { get; set; } 
     
        public FilterExpression<DateTime>? CreatedDate { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedDate { get; set; } 
     
        public FilterExpression<string?>? UserName { get; set; } 
     
        public FilterExpression<string?>? NormalizedUserName { get; set; } 
     
        public FilterExpression<string?>? Email { get; set; } 
     
        public FilterExpression<string?>? NormalizedEmail { get; set; } 
     
        public FilterExpression<bool>? EmailConfirmed { get; set; } 
     
        public FilterExpression<string?>? PasswordHash { get; set; } 
     
        public FilterExpression<string?>? SecurityStamp { get; set; } 
     
        public FilterExpression<string?>? ConcurrencyStamp { get; set; } 
     
        public FilterExpression<string?>? PhoneNumber { get; set; } 
     
        public FilterExpression<bool>? PhoneNumberConfirmed { get; set; } 
     
        public FilterExpression<bool>? TwoFactorEnabled { get; set; } 
     
        public FilterExpression<DateTimeOffset?>? LockoutEnd { get; set; } 
     
        public FilterExpression<bool>? LockoutEnabled { get; set; } 
     
        public FilterExpression<int>? AccessFailedCount { get; set; } 
     }

    public class AddAspNetUserReqDto : IMapTo<AspNetUser>
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

    public class UpdateAspNetUserReqDto :AddAspNetUserReqDto, IMapTo<AspNetUser>
    {
     }

}

