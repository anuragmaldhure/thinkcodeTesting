
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class AspNetUserHelper 
    {
        private static Faker<AspNetUser> AspNetUserFaker = new Faker<AspNetUser>()
            .RuleFor(p => p.Id, f => f.Lorem.Sentence())
            .RuleFor(p => p.FirstName, f => f.Name.FullName())
            .RuleFor(p => p.LastName, f => f.Name.FullName())
            .RuleFor(p => p.IsActive, f => f.Random.Bool())
            .RuleFor(p => p.CreatedDate, f => f.Date.Recent())
            .RuleFor(p => p.UpdatedDate, f => f.Date.Recent())
            .RuleFor(p => p.UserName, f => f.Name.FullName())
            .RuleFor(p => p.NormalizedUserName, f => f.Name.FullName())
            .RuleFor(p => p.Email, f => f.Lorem.Sentence())
            .RuleFor(p => p.NormalizedEmail, f => f.Lorem.Sentence())
            .RuleFor(p => p.EmailConfirmed, f => f.Random.Bool())
            .RuleFor(p => p.PasswordHash, f => f.Lorem.Sentence())
            .RuleFor(p => p.SecurityStamp, f => f.Lorem.Sentence())
            .RuleFor(p => p.ConcurrencyStamp, f => f.Lorem.Sentence())
            .RuleFor(p => p.PhoneNumber, f => f.Lorem.Sentence())
            .RuleFor(p => p.PhoneNumberConfirmed, f => f.Random.Bool())
            .RuleFor(p => p.TwoFactorEnabled, f => f.Random.Bool())
            .RuleFor(p => p.LockoutEnd, f => f.Date.RecentOffset())
            .RuleFor(p => p.LockoutEnabled, f => f.Random.Bool())
            .RuleFor(p => p.AccessFailedCount, f => f.Random.Int())
;

        public static async Task<AspNetUser> AddAspNetUser(AppDbContext appDbContext)
        {
            var aspNetUser = AspNetUserFaker.Generate();
            var entry = await appDbContext.AspNetUsers.AddAsync(aspNetUser);
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, AspNetUser aspNetUser)
        {
			appDbContext.AspNetUsers.Remove(aspNetUser);
		}

    }
}

