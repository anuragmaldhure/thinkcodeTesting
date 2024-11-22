namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IServicesCollection
{
    IAspNetRoleClaimService AspNetRoleClaimServices { get; }
    IAspNetRoleService AspNetRoleServices { get; }
    IAspNetUserClaimService AspNetUserClaimServices { get; }
    IAspNetUserService AspNetUserServices { get; }
    ITeamService TeamServices { get; }
    IToDoChecklistService ToDoChecklistServices { get; }
    IToDoMasterService ToDoMasterServices { get; }
    IToDoReminderService ToDoReminderServices { get; }
}
