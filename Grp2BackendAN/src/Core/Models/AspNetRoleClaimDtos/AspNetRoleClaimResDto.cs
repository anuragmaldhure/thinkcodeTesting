
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetRoleClaimResDto:  IMapFrom<AspNetRoleClaim>
    {     
        
        public int Id { get; set; }
        
        public string RoleId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
    }

   public class AspNetRoleClaimResDetailDto: AspNetRoleClaimResDto, IMapFrom<AspNetRoleClaim>
   {
   
        public virtual AspNetRoleResDto? AspNetRoleClaimRoleIdfk { get; set; }
   }


}

