
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class ToDoMasterHelper 
    {
        private static Faker<ToDoMaster> ToDoMasterFaker = new Faker<ToDoMaster>()
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.IsComplete, f => f.Random.Bool())
            .RuleFor(p => p.RepeatFrequency, f => f.Lorem.Sentence())
            .RuleFor(p => p.CreatedDate, f => f.Date.Recent())
            .RuleFor(p => p.DueDate, f => f.Date.Recent())
            .RuleFor(p => p.UpdatedDate, f => f.Date.Recent())
            .RuleFor(p => p.CompletedDate, f => f.Date.Recent())
            .RuleFor(p => p.AssignedToId, f => f.Lorem.Sentence())
            .RuleFor(p => p.IsAssignedToTeam, f => f.Random.Bool())
            .RuleFor(p => p.HasChecklist, f => f.Random.Bool())
            .RuleFor(p => p.HasReminder, f => f.Random.Bool())
            .RuleFor(p => p.PercentageCompleted, f => f.Random.Decimal())
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool())
            .RuleFor(p => p.IsStarred, f => f.Random.Bool())
            .RuleFor(p => p.MediaAttachmentType, f => f.Lorem.Sentence())
            .RuleFor(p => p.MediaAttachmentURL, f => f.Lorem.Sentence())
;

        public static async Task<ToDoMaster> AddToDoMaster(AppDbContext appDbContext, AspNetUser toDoMasterCreatedByIdfk = null)
        {
            var toDoMaster = ToDoMasterFaker.Generate();
   		
			toDoMasterCreatedByIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            toDoMaster.CreatedById = (string)toDoMasterCreatedByIdfk?.Id;
            var entry = await appDbContext.ToDoMasters.AddAsync(toDoMaster);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, ToDoMaster toDoMaster)
        {
   		
            AspNetUser aspNetUser = toDoMaster.ToDoMasterCreatedByIdfk;		
			appDbContext.ToDoMasters.Remove(toDoMaster);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

