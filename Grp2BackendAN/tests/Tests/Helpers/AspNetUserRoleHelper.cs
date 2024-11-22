
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetUserRoleHelper 
    {
        private static Faker<AspNetUserRole> AspNetUserRoleFaker = new Faker<AspNetUserRole>()
            .RuleFor(p => p.UserId, f => f.Lorem.Sentence())
;

        public static async Task<AspNetUserRole> AddAspNetUserRole(AppDbContext appDbContext, AspNetUser aspNetUserRoleUserIdfk = null , AspNetRole aspNetUserRoleRoleIdfk = null)
        {
            var aspNetUserRole = AspNetUserRoleFaker.Generate();
   		
			aspNetUserRoleUserIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
   		
			aspNetUserRoleRoleIdfk ??= await AspNetRoleHelper.AddAspNetRole(appDbContext);
            aspNetUserRole.UserId = (string)aspNetUserRoleUserIdfk?.Id;
            aspNetUserRole.RoleId = (string)aspNetUserRoleRoleIdfk?.Id;
            var entry = await appDbContext.AspNetUserRoles.AddAsync(aspNetUserRole);
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetUserRole aspNetUserRole)
        {
   		
            AspNetUser aspNetUser = aspNetUserRole.AspNetUserRoleUserIdfk;		
   		
            AspNetRole aspNetRole = aspNetUserRole.AspNetUserRoleRoleIdfk;		
			appDbContext.AspNetUserRoles.Remove(aspNetUserRole);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
   	
            if(aspNetRole != null)
			    await AspNetRoleHelper.CleanUp(appDbContext, aspNetRole);
		}

    }
}

