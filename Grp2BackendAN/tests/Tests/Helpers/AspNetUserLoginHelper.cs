
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetUserLoginHelper 
    {
        private static Faker<AspNetUserLogin> AspNetUserLoginFaker = new Faker<AspNetUserLogin>()
            .RuleFor(p => p.LoginProvider, f => f.Lorem.Sentence())
            .RuleFor(p => p.ProviderKey, f => f.Lorem.Sentence())
            .RuleFor(p => p.ProviderDisplayName, f => f.Name.FullName())
;

        public static async Task<AspNetUserLogin> AddAspNetUserLogin(AppDbContext appDbContext, AspNetUser aspNetUserLoginUserIdfk = null)
        {
            var aspNetUserLogin = AspNetUserLoginFaker.Generate();
   		
			aspNetUserLoginUserIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            aspNetUserLogin.UserId = (string)aspNetUserLoginUserIdfk?.Id;
            var entry = await appDbContext.AspNetUserLogins.AddAsync(aspNetUserLogin);
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetUserLogin aspNetUserLogin)
        {
   		
            AspNetUser aspNetUser = aspNetUserLogin.AspNetUserLoginUserIdfk;		
			appDbContext.AspNetUserLogins.Remove(aspNetUserLogin);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

