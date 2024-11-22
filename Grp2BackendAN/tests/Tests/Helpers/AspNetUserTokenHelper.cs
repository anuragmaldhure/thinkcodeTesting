
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetUserTokenHelper 
    {
        private static Faker<AspNetUserToken> AspNetUserTokenFaker = new Faker<AspNetUserToken>()
            .RuleFor(p => p.UserId, f => f.Lorem.Sentence())
            .RuleFor(p => p.LoginProvider, f => f.Lorem.Sentence())
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.Value, f => f.Lorem.Sentence())
;

        public static async Task<AspNetUserToken> AddAspNetUserToken(AppDbContext appDbContext, AspNetUser aspNetUserTokenUserIdfk = null)
        {
            var aspNetUserToken = AspNetUserTokenFaker.Generate();
   		
			aspNetUserTokenUserIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            aspNetUserToken.UserId = (string)aspNetUserTokenUserIdfk?.Id;
            var entry = await appDbContext.AspNetUserTokens.AddAsync(aspNetUserToken);
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetUserToken aspNetUserToken)
        {
   		
            AspNetUser aspNetUser = aspNetUserToken.AspNetUserTokenUserIdfk;		
			appDbContext.AspNetUserTokens.Remove(aspNetUserToken);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

