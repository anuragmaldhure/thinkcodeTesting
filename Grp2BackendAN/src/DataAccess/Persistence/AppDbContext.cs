
namespace thinkbridge.Grp2BackendAN.DataAccess.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder = optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntityConfigurations();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    public DbSet<AspNetRole> AspNetRoles { get; set; }
    public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
    public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
    public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
    public DbSet<AspNetUser> AspNetUsers { get; set; }
    public DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamsUsersMapping> TeamsUsersMappings { get; set; }
    public DbSet<ToDoChecklist> ToDoChecklists { get; set; }
    public DbSet<ToDoMaster> ToDoMasters { get; set; }
    public DbSet<ToDoReminder> ToDoReminders { get; set; }
}
