
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetUserTokenReqDto : BasePagination
    {     
     
        public FilterExpression<string>? UserId { get; set; } 
     
        public FilterExpression<string>? LoginProvider { get; set; } 
     
        public FilterExpression<string>? Name { get; set; } 
     
        public FilterExpression<string?>? Value { get; set; } 
     }

    public class AddAspNetUserTokenReqDto : IMapTo<AspNetUserToken>
    {
        
        public string UserId { get; set; }
        
        public string LoginProvider { get; set; }
        
        public string Name { get; set; }
        
        public string? Value { get; set; }
    }

    public class UpdateAspNetUserTokenReqDto :AddAspNetUserTokenReqDto, IMapTo<AspNetUserToken>
    {
     }

}

