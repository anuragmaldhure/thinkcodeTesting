
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetRoleReqDto : BasePagination
    {     
     
        public FilterExpression<string>? Id { get; set; } 
     
        public FilterExpression<string?>? Name { get; set; } 
     
        public FilterExpression<string?>? NormalizedName { get; set; } 
     
        public FilterExpression<string?>? ConcurrencyStamp { get; set; } 
     }

    public class AddAspNetRoleReqDto : IMapTo<AspNetRole>
    {
        
        public string Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? NormalizedName { get; set; }
        
        public string? ConcurrencyStamp { get; set; }
    }

    public class UpdateAspNetRoleReqDto :AddAspNetRoleReqDto, IMapTo<AspNetRole>
    {
     }

}

