
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetRoleHelper 
    {
        private static Faker<AspNetRole> AspNetRoleFaker = new Faker<AspNetRole>()
            .RuleFor(p => p.Id, f => f.Lorem.Sentence())
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.NormalizedName, f => f.Name.FullName())
            .RuleFor(p => p.ConcurrencyStamp, f => f.Lorem.Sentence())
;

        public static async Task<AspNetRole> AddAspNetRole(AppDbContext appDbContext)
        {
            var aspNetRole = AspNetRoleFaker.Generate();
            var entry = await appDbContext.AspNetRoles.AddAsync(aspNetRole);
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetRole aspNetRole)
        {
			appDbContext.AspNetRoles.Remove(aspNetRole);
		}

    }
}

