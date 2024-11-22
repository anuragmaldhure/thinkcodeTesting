
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetRoleClaimHelper 
    {
        private static Faker<AspNetRoleClaim> AspNetRoleClaimFaker = new Faker<AspNetRoleClaim>()
            .RuleFor(p => p.ClaimType, f => f.Lorem.Sentence())
            .RuleFor(p => p.ClaimValue, f => f.Lorem.Sentence())
;

        public static async Task<AspNetRoleClaim> AddAspNetRoleClaim(AppDbContext appDbContext, AspNetRole aspNetRoleClaimRoleIdfk = null)
        {
            var aspNetRoleClaim = AspNetRoleClaimFaker.Generate();
   		
			aspNetRoleClaimRoleIdfk ??= await AspNetRoleHelper.AddAspNetRole(appDbContext);
            aspNetRoleClaim.RoleId = (string)aspNetRoleClaimRoleIdfk?.Id;
            var entry = await appDbContext.AspNetRoleClaims.AddAsync(aspNetRoleClaim);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetRoleClaim aspNetRoleClaim)
        {
   		
            AspNetRole aspNetRole = aspNetRoleClaim.AspNetRoleClaimRoleIdfk;		
			appDbContext.AspNetRoleClaims.Remove(aspNetRoleClaim);
   	
            if(aspNetRole != null)
			    await AspNetRoleHelper.CleanUp(appDbContext, aspNetRole);
		}

    }
}

