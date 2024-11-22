
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetUserLoginReqDto : BasePagination
    {     
     
        public FilterExpression<string>? LoginProvider { get; set; } 
     
        public FilterExpression<string>? ProviderKey { get; set; } 
     
        public FilterExpression<string?>? ProviderDisplayName { get; set; } 
     
        public FilterExpression<string>? UserId { get; set; } 
     }

    public class AddAspNetUserLoginReqDto : IMapTo<AspNetUserLogin>
    {
        
        public string LoginProvider { get; set; }
        
        public string ProviderKey { get; set; }
        
        public string? ProviderDisplayName { get; set; }
        
        public string UserId { get; set; }
    }

    public class UpdateAspNetUserLoginReqDto :AddAspNetUserLoginReqDto, IMapTo<AspNetUserLogin>
    {
     }

}

