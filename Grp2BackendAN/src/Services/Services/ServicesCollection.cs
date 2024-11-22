namespace thinkbridge.Grp2BackendAN.Services.Services;
public class ServicesCollection : IServicesCollection, IDisposable
{
    private readonly IServiceScope _scope;
    public ServicesCollection(IServiceFactory serviceFactory)
    {
        _scope = serviceFactory.CreateScope();
        _lazyIAspNetRoleClaimService = new Lazy<IAspNetRoleClaimService>(_scope.ServiceProvider.GetRequiredService<IAspNetRoleClaimService>);
        _lazyIAspNetRoleService = new Lazy<IAspNetRoleService>(_scope.ServiceProvider.GetRequiredService<IAspNetRoleService>);
        _lazyIAspNetUserClaimService = new Lazy<IAspNetUserClaimService>(_scope.ServiceProvider.GetRequiredService<IAspNetUserClaimService>);
        _lazyIAspNetUserService = new Lazy<IAspNetUserService>(_scope.ServiceProvider.GetRequiredService<IAspNetUserService>);
        _lazyITeamService = new Lazy<ITeamService>(_scope.ServiceProvider.GetRequiredService<ITeamService>);
        _lazyIToDoChecklistService = new Lazy<IToDoChecklistService>(_scope.ServiceProvider.GetRequiredService<IToDoChecklistService>);
        _lazyIToDoMasterService = new Lazy<IToDoMasterService>(_scope.ServiceProvider.GetRequiredService<IToDoMasterService>);
        _lazyIToDoReminderService = new Lazy<IToDoReminderService>(_scope.ServiceProvider.GetRequiredService<IToDoReminderService>);
    }

    private readonly Lazy<IAspNetRoleClaimService> _lazyIAspNetRoleClaimService;
    private readonly Lazy<IAspNetRoleService> _lazyIAspNetRoleService;
    private readonly Lazy<IAspNetUserClaimService> _lazyIAspNetUserClaimService;
    private readonly Lazy<IAspNetUserService> _lazyIAspNetUserService;
    private readonly Lazy<ITeamService> _lazyITeamService;
    private readonly Lazy<IToDoChecklistService> _lazyIToDoChecklistService;
    private readonly Lazy<IToDoMasterService> _lazyIToDoMasterService;
    private readonly Lazy<IToDoReminderService> _lazyIToDoReminderService;
    public void Dispose()
    {
        _scope?.Dispose();
    }
    public IAspNetRoleClaimService AspNetRoleClaimServices => _lazyIAspNetRoleClaimService.Value;
    public IAspNetRoleService AspNetRoleServices => _lazyIAspNetRoleService.Value;
    public IAspNetUserClaimService AspNetUserClaimServices => _lazyIAspNetUserClaimService.Value;
    public IAspNetUserService AspNetUserServices => _lazyIAspNetUserService.Value;
    public ITeamService TeamServices => _lazyITeamService.Value;
    public IToDoChecklistService ToDoChecklistServices => _lazyIToDoChecklistService.Value;
    public IToDoMasterService ToDoMasterServices => _lazyIToDoMasterService.Value;
    public IToDoReminderService ToDoReminderServices => _lazyIToDoReminderService.Value;
}
