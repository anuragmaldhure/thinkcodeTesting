
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class GetAllAspNetUserRoleReqDto : BasePagination
    {     
     
        public FilterExpression<string>? UserId { get; set; } 
     
        public FilterExpression<string>? RoleId { get; set; } 
     }

    public class AddAspNetUserRoleReqDto : IMapTo<AspNetUserRole>
    {
        
        public string UserId { get; set; }
        
        public string RoleId { get; set; }
    }

    public class UpdateAspNetUserRoleReqDto :AddAspNetUserRoleReqDto, IMapTo<AspNetUserRole>
    {
     }

}

