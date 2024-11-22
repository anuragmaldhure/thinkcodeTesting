
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetUserClaimHelper 
    {
        private static Faker<AspNetUserClaim> AspNetUserClaimFaker = new Faker<AspNetUserClaim>()
            .RuleFor(p => p.ClaimType, f => f.Lorem.Sentence())
            .RuleFor(p => p.ClaimValue, f => f.Lorem.Sentence())
;

        public static async Task<AspNetUserClaim> AddAspNetUserClaim(AppDbContext appDbContext, AspNetUser aspNetUserClaimUserIdfk = null)
        {
            var aspNetUserClaim = AspNetUserClaimFaker.Generate();
   		
			aspNetUserClaimUserIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            aspNetUserClaim.UserId = (string)aspNetUserClaimUserIdfk?.Id;
            var entry = await appDbContext.AspNetUserClaims.AddAsync(aspNetUserClaim);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetUserClaim aspNetUserClaim)
        {
   		
            AspNetUser aspNetUser = aspNetUserClaim.AspNetUserClaimUserIdfk;		
			appDbContext.AspNetUserClaims.Remove(aspNetUserClaim);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

