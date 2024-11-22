
namespace thinkbridge.Grp2BackendAN.Core.Models
{

    public class AspNetUserTokenResDto:  IMapFrom<AspNetUserToken>
    {     
        
        public string UserId { get; set; }
        
        public string LoginProvider { get; set; }
        
        public string Name { get; set; }
        
        public string? Value { get; set; }
    }

   public class AspNetUserTokenResDetailDto: AspNetUserTokenResDto, IMapFrom<AspNetUserToken>
   {
   
        public virtual AspNetUserResDto? AspNetUserTokenUserIdfk { get; set; }
   }


}

