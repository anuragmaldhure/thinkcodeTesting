
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetUserLoginResDto:  IMapFrom<AspNetUserLogin>
    {     
        
        public string LoginProvider { get; set; }
        
        public string ProviderKey { get; set; }
        
        public string? ProviderDisplayName { get; set; }
        
        public string UserId { get; set; }
    }

   public class AspNetUserLoginResDetailDto: AspNetUserLoginResDto, IMapFrom<AspNetUserLogin>
   {
   
        public virtual AspNetUserResDto? AspNetUserLoginUserIdfk { get; set; }
   }


}

