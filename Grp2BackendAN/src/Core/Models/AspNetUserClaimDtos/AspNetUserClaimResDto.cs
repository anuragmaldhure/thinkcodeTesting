
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetUserClaimResDto:  IMapFrom<AspNetUserClaim>
    {     
        
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public string? ClaimType { get; set; }
        
        public string? ClaimValue { get; set; }
    }

   public class AspNetUserClaimResDetailDto: AspNetUserClaimResDto, IMapFrom<AspNetUserClaim>
   {
   
        public virtual AspNetUserResDto? AspNetUserClaimUserIdfk { get; set; }
   }


}

