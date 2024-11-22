
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class ToDoChecklistHelper 
    {
        private static Faker<ToDoChecklist> ToDoChecklistFaker = new Faker<ToDoChecklist>()
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.IsComplete, f => f.Random.Bool())
            .RuleFor(p => p.CreatedDate, f => f.Date.Recent())
            .RuleFor(p => p.UpdatedDate, f => f.Date.Recent())
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool())
;

        public static async Task<ToDoChecklist> AddToDoChecklist(AppDbContext appDbContext, ToDoMaster toDoChecklistToDoTaskIdfk = null)
        {
            var toDoChecklist = ToDoChecklistFaker.Generate();
   		
			toDoChecklistToDoTaskIdfk ??= await ToDoMasterHelper.AddToDoMaster(appDbContext);
            toDoChecklist.ToDoTaskId = (int)toDoChecklistToDoTaskIdfk?.ToDoTaskId;
            var entry = await appDbContext.ToDoChecklists.AddAsync(toDoChecklist);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, ToDoChecklist toDoChecklist)
        {
   		
            ToDoMaster toDoMaster = toDoChecklist.ToDoChecklistToDoTaskIdfk;		
			appDbContext.ToDoChecklists.Remove(toDoChecklist);
   	
            if(toDoMaster != null)
			    await ToDoMasterHelper.CleanUp(appDbContext, toDoMaster);
		}

    }
}

