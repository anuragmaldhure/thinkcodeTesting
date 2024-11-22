
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class TeamsUsersMappingHelper 
    {
        private static Faker<TeamsUsersMapping> TeamsUsersMappingFaker = new Faker<TeamsUsersMapping>()
            .RuleFor(p => p.TeamId, f => f.Random.Int())
            .RuleFor(p => p.IsActive, f => f.Random.Bool())
            .RuleFor(p => p.AddedAt, f => f.Date.Recent())
;

        public static async Task<TeamsUsersMapping> AddTeamsUsersMapping(AppDbContext appDbContext, Team teamsUsersMappingTeamIdfk = null , AspNetUser teamsUsersMappingUserIdfk = null , AspNetUser teamsUsersMappingAddedByIdfk = null)
        {
            var teamsUsersMapping = TeamsUsersMappingFaker.Generate();
   		
			teamsUsersMappingTeamIdfk ??= await TeamHelper.AddTeam(appDbContext);
   		
			teamsUsersMappingUserIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
   		
			teamsUsersMappingAddedByIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            teamsUsersMapping.TeamId = (int)teamsUsersMappingTeamIdfk?.TeamId;
            teamsUsersMapping.UserId = (string)teamsUsersMappingUserIdfk?.Id;
            teamsUsersMapping.AddedById = (string)teamsUsersMappingAddedByIdfk?.Id;
            var entry = await appDbContext.TeamsUsersMappings.AddAsync(teamsUsersMapping);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, TeamsUsersMapping teamsUsersMapping)
        {
   		
            Team team = teamsUsersMapping.TeamsUsersMappingTeamIdfk;		
   		
            AspNetUser aspNetUser = teamsUsersMapping.TeamsUsersMappingUserIdfk;		
   		
            AspNetUser aspNetUser = teamsUsersMapping.TeamsUsersMappingAddedByIdfk;		
			appDbContext.TeamsUsersMappings.Remove(teamsUsersMapping);
   	
            if(team != null)
			    await TeamHelper.CleanUp(appDbContext, team);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

