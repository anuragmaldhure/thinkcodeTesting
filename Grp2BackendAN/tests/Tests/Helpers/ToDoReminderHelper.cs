
namespace thinkbridge.Grp2BackendAN.Tests.Helpers
{
    public class ToDoReminderHelper 
    {
        private static Faker<ToDoReminder> ToDoReminderFaker = new Faker<ToDoReminder>()
            .RuleFor(p => p.SetForDateTime, f => f.Date.Recent())
            .RuleFor(p => p.SentAtDateTime, f => f.Date.Recent())
            .RuleFor(p => p.IsActive, f => f.Random.Bool())
;

        public static async Task<ToDoReminder> AddToDoReminder(AppDbContext appDbContext, ToDoMaster toDoReminderToDoTaskIdfk = null , AspNetUser toDoReminderSetByIdfk = null)
        {
            var toDoReminder = ToDoReminderFaker.Generate();
   		
			toDoReminderToDoTaskIdfk ??= await ToDoMasterHelper.AddToDoMaster(appDbContext);
   		
			toDoReminderSetByIdfk ??= await AspNetUserHelper.AddAspNetUser(appDbContext);
            toDoReminder.ToDoTaskId = (int)toDoReminderToDoTaskIdfk?.ToDoTaskId;
            toDoReminder.SetById = (string)toDoReminderSetByIdfk?.Id;
            var entry = await appDbContext.ToDoReminders.AddAsync(toDoReminder);
            appDbContext.SaveChanges();
            return entry.Entity;
        }

        public static async Task CleanUp(AppDbContext appDbContext, ToDoReminder toDoReminder)
        {
   		
            ToDoMaster toDoMaster = toDoReminder.ToDoReminderToDoTaskIdfk;		
   		
            AspNetUser aspNetUser = toDoReminder.ToDoReminderSetByIdfk;		
			appDbContext.ToDoReminders.Remove(toDoReminder);
   	
            if(toDoMaster != null)
			    await ToDoMasterHelper.CleanUp(appDbContext, toDoMaster);
   	
            if(aspNetUser != null)
			    await AspNetUserHelper.CleanUp(appDbContext, aspNetUser);
		}

    }
}

