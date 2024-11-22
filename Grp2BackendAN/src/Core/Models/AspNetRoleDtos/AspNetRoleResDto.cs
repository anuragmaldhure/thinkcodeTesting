
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetRoleResDto:  IMapFrom<AspNetRole>
    {     
        
        public string Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? NormalizedName { get; set; }
        
        public string? ConcurrencyStamp { get; set; }
    }

   public class AspNetRoleResDetailDto: AspNetRoleResDto, IMapFrom<AspNetRole>
   {
        public virtual List<AspNetRoleClaimResDto>? AspNetRoleClaimRoles { get; set; }
     public virtual List<AspNetUserRoleResDto>? AspNetUserRoleRoles { get; set; }

   }


}

