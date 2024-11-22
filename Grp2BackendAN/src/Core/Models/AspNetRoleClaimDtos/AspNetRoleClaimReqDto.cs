
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetRoleClaimReqDto : BasePagination
    {     
     
        public FilterExpression<int>? Id { get; set; } 
     
        public FilterExpression<string>? RoleId { get; set; } 
     
        public FilterExpression<string?>? ClaimType { get; set; } 
     
        public FilterExpression<string?>? ClaimValue { get; set; } 
     }

    public class AddAspNetRoleClaimReqDto : IMapTo<AspNetRoleClaim>
    {
        
        public string RoleId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
    }

    public class UpdateAspNetRoleClaimReqDto :AddAspNetRoleClaimReqDto, IMapTo<AspNetRoleClaim>
    {
        public int Id { get; set; }
    }

}

