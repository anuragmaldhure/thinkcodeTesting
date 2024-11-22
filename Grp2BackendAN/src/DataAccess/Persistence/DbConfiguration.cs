
namespace thinkbridge.Grp2BackendAN.DataAccess.Persistence;
public static class DbConfiguration
{
    public static void ApplyEntityConfigurations(this ModelBuilder builder)
    {
        builder.Entity<AspNetRoleClaim>(entity =>
        {
entity.HasKey(e => new { e.Id });
        entity.Property(x => x.Id).ValueGeneratedOnAdd();
        entity.Property(x => x.Id).HasColumnType("int").IsRequired();
        entity.Property(x => x.RoleId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.ClaimType).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.ClaimValue).HasColumnType("nvarchar").HasMaxLength(255);
        entity.HasIndex(x => new { x.RoleId }).IsUnique(false).HasDatabaseName("IX_AspNetRoleClaims_RoleId");
        entity.HasOne(x => x.AspNetRoleClaimRoleIdfk).WithMany(x => x.AspNetRoleClaimRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);

});
builder.Entity<AspNetRole>(entity =>
{
entity.HasKey(e => new { e.Id });
        entity.Property(x => x.Id);
        entity.Property(x => x.Id).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.NormalizedName).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.ConcurrencyStamp).HasColumnType("nvarchar").HasMaxLength(255);
        entity.HasIndex(x => new { x.NormalizedName }).IsUnique(true).HasDatabaseName("RoleNameIndex");

});
builder.Entity<AspNetUserClaim>(entity =>
{
entity.HasKey(e => new { e.Id });
        entity.Property(x => x.Id).ValueGeneratedOnAdd();
        entity.Property(x => x.Id).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.ClaimType).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.ClaimValue).HasColumnType("nvarchar").HasMaxLength(255);
        entity.HasIndex(x => new { x.UserId }).IsUnique(false).HasDatabaseName("IX_AspNetUserClaims_UserId");
        entity.HasOne(x => x.AspNetUserClaimUserIdfk).WithMany(x => x.AspNetUserClaimUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

});
builder.Entity<AspNetUserLogin>(entity =>
{
entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        entity.Property(x => x.ProviderKey);
        entity.Property(x => x.LoginProvider);
        entity.Property(x => x.LoginProvider).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.ProviderKey).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.ProviderDisplayName).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.HasIndex(x => new { x.UserId }).IsUnique(false).HasDatabaseName("IX_AspNetUserLogins_UserId");
        entity.HasOne(x => x.AspNetUserLoginUserIdfk).WithMany(x => x.AspNetUserLoginUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

});
builder.Entity<AspNetUserRole>(entity =>
{
entity.HasKey(e => new { e.UserId, e.RoleId });
        entity.Property(x => x.RoleId);
        entity.Property(x => x.UserId);
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.RoleId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.HasOne(x => x.AspNetUserRoleUserIdfk).WithMany(x => x.AspNetUserRoleUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(x => x.AspNetUserRoleRoleIdfk).WithMany(x => x.AspNetUserRoleRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);

});
builder.Entity<AspNetUser>(entity =>
{
entity.HasKey(e => new { e.Id });
        entity.Property(x => x.Id);
        entity.Property(x => x.Id).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        entity.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UserName).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.NormalizedUserName).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.NormalizedEmail).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.EmailConfirmed).HasColumnType("bit").IsRequired();
        entity.Property(x => x.PasswordHash).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.SecurityStamp).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.ConcurrencyStamp).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.PhoneNumberConfirmed).HasColumnType("bit").IsRequired();
        entity.Property(x => x.TwoFactorEnabled).HasColumnType("bit").IsRequired();
        entity.Property(x => x.LockoutEnd).HasColumnType("datetimeoffset");
        entity.Property(x => x.LockoutEnabled).HasColumnType("bit").IsRequired();
        entity.Property(x => x.AccessFailedCount).HasColumnType("int").IsRequired();
        entity.HasIndex(x => new { x.NormalizedEmail }).IsUnique(false).HasDatabaseName("EmailIndex");
        entity.HasIndex(x => new { x.NormalizedUserName }).IsUnique(true).HasDatabaseName("UserNameIndex");

});
builder.Entity<AspNetUserToken>(entity =>
{
entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        entity.Property(x => x.Name);
        entity.Property(x => x.LoginProvider);
        entity.Property(x => x.UserId);
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.LoginProvider).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.Value).HasColumnType("nvarchar").HasMaxLength(255);
        entity.HasOne(x => x.AspNetUserTokenUserIdfk).WithMany(x => x.AspNetUserTokenUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

});
builder.Entity<Team>(entity =>
{
entity.HasKey(e => new { e.TeamId });
        entity.Property(x => x.TeamId).ValueGeneratedOnAdd();
        entity.Property(x => x.TeamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.TeamName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.CreatedById).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        entity.HasIndex(x => new { x.CreatedById }).IsUnique(false).HasDatabaseName("IX_Teams_CreatedById");
        entity.HasOne(x => x.TeamCreatedByIdfk).WithMany(x => x.TeamCreatedBies).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<TeamsUsersMapping>(entity =>
{
entity.HasKey(e => new { e.TeamId, e.UserId });
        entity.Property(x => x.UserId);
        entity.Property(x => x.TeamId);
        entity.Property(x => x.TeamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        entity.Property(x => x.AddedAt).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.AddedById).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.HasIndex(x => new { x.AddedById }).IsUnique(false).HasDatabaseName("IX_TeamsUsersMappings_AddedById");
        entity.HasOne(x => x.TeamsUsersMappingTeamIdfk).WithMany(x => x.TeamsUsersMappingTeams).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.TeamsUsersMappingUserIdfk).WithMany(x => x.TeamsUsersMappingUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.TeamsUsersMappingAddedByIdfk).WithMany(x => x.TeamsUsersMappingAddedBies).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<ToDoChecklist>(entity =>
{
entity.HasKey(e => new { e.ToDoChecklistId });
        entity.Property(x => x.ToDoChecklistId).ValueGeneratedOnAdd();
        entity.Property(x => x.ToDoChecklistId).HasColumnType("int").IsRequired();
        entity.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsComplete).HasColumnType("bit").IsRequired();
        entity.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedDate).HasColumnType("datetime2");
        entity.Property(x => x.ToDoTaskId).HasColumnType("int").IsRequired();
        entity.Property(x => x.IsDeleted).HasColumnType("bit").IsRequired();
        entity.HasIndex(x => new { x.ToDoTaskId }).IsUnique(false).HasDatabaseName("IX_ToDoChecklists_ToDoTaskId");
        entity.HasOne(x => x.ToDoChecklistToDoTaskIdfk).WithMany(x => x.ToDoChecklistToDoTasks).HasForeignKey(x => x.ToDoTaskId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<ToDoMaster>(entity =>
{
entity.HasKey(e => new { e.ToDoTaskId });
        entity.Property(x => x.ToDoTaskId).ValueGeneratedOnAdd();
        entity.Property(x => x.ToDoTaskId).HasColumnType("int").IsRequired();
        entity.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsComplete).HasColumnType("bit").IsRequired();
        entity.Property(x => x.RepeatFrequency).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.DueDate).HasColumnType("datetime2");
        entity.Property(x => x.UpdatedDate).HasColumnType("datetime2");
        entity.Property(x => x.CompletedDate).HasColumnType("datetime2");
        entity.Property(x => x.CreatedById).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.AssignedToId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.IsAssignedToTeam).HasColumnType("bit").IsRequired();
        entity.Property(x => x.HasChecklist).HasColumnType("bit").IsRequired();
        entity.Property(x => x.HasReminder).HasColumnType("bit").IsRequired();
        entity.Property(x => x.PercentageCompleted).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.IsDeleted).HasColumnType("bit").IsRequired();
        entity.Property(x => x.IsStarred).HasColumnType("bit").IsRequired();
        entity.Property(x => x.MediaAttachmentType).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.MediaAttachmentURL).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.HasIndex(x => new { x.AssignedToId }).IsUnique(false).HasDatabaseName("IX_ToDoMasters_AssignedToId");
        entity.HasIndex(x => new { x.CreatedById }).IsUnique(false).HasDatabaseName("IX_ToDoMasters_CreatedById");
        entity.HasOne(x => x.ToDoMasterCreatedByIdfk).WithMany(x => x.ToDoMasterCreatedBies).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<ToDoReminder>(entity =>
{
entity.HasKey(e => new { e.ReminderId });
        entity.Property(x => x.ReminderId).ValueGeneratedOnAdd();
        entity.Property(x => x.ReminderId).HasColumnType("int").IsRequired();
        entity.Property(x => x.ToDoTaskId).HasColumnType("int").IsRequired();
        entity.Property(x => x.SetById).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.SetForDateTime).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.SentAtDateTime).HasColumnType("datetime2");
        entity.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        entity.HasIndex(x => new { x.SetById }).IsUnique(false).HasDatabaseName("IX_ToDoReminders_SetById");
        entity.HasIndex(x => new { x.ToDoTaskId }).IsUnique(false).HasDatabaseName("IX_ToDoReminders_ToDoTaskId");
        entity.HasOne(x => x.ToDoReminderToDoTaskIdfk).WithMany(x => x.ToDoReminderToDoTasks).HasForeignKey(x => x.ToDoTaskId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.ToDoReminderSetByIdfk).WithMany(x => x.ToDoReminderSetBies).HasForeignKey(x => x.SetById).OnDelete(DeleteBehavior.NoAction);

});
        // add entity configurations here 
    }
}
