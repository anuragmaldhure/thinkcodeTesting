
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetUserClaimReqDto : BasePagination
    {     
     
        public FilterExpression<int>? Id { get; set; } 
     
        public FilterExpression<string>? UserId { get; set; } 
     
        public FilterExpression<string?>? ClaimType { get; set; } 
     
        public FilterExpression<string?>? ClaimValue { get; set; } 
     }

    public class AddAspNetUserClaimReqDto : IMapTo<AspNetUserClaim>
    {
        
        public string UserId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
    }

    public class UpdateAspNetUserClaimReqDto :AddAspNetUserClaimReqDto, IMapTo<AspNetUserClaim>
    {
        public int Id { get; set; }
    }

}

