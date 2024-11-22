
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetUserRoleResDto:  IMapFrom<AspNetUserRole>
    {     
        
        public string UserId { get; set; }
        
        public string RoleId { get; set; }
    }

   public class AspNetUserRoleResDetailDto: AspNetUserRoleResDto, IMapFrom<AspNetUserRole>
   {
   
        public virtual AspNetUserResDto? AspNetUserRoleUserIdfk { get; set; }
        public virtual AspNetRoleResDto? AspNetUserRoleRoleIdfk { get; set; }
   }


}

