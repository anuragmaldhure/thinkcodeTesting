
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class TeamHelper 
    {
        private static Faker<Team> TeamFaker = new Faker<Team>()
            .RuleFor(p => p.TeamName, f => f.Name.FullName())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.CreatedAt, f => f.Date.Recent())
            .RuleFor(p => p.IsActive, f => f.Random.Bool())
;

        public static async Task<Team> AddTeam(AppDbContext appDbContext, AspNetUser teamCreatedByIdfk = null)
        {
            var team = TeamFaker.Generate();
   		
			teamCreatedByIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            team.CreatedById = (string)teamCreatedByIdfk?.Id;
            var entry = await appDbContext.Teams.AddAsync(team);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, Team team)
        {
   		
            AspNetUser aspNetUser = team.TeamCreatedByIdfk;		
			appDbContext.Teams.Remove(team);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

