namespace thinkbridge.Grp2BackendAN.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the service factory
        services.AddSingleton<IServiceFactory, ServiceFactory>();
        // ServicesCollection Registration 
        services.AddScoped<IServicesCollection, ServicesCollection>();
        // data access Layer
        services.AddDataAccessLayer(configuration);
        services.AddScoped<IAspNetRoleClaimService, AspNetRoleClaimService>();
        services.AddScoped<IAspNetRoleService, AspNetRoleService>();
        services.AddScoped<IAspNetUserClaimService, AspNetUserClaimService>();
        services.AddScoped<IAspNetUserService, AspNetUserService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IToDoChecklistService, ToDoChecklistService>();
        services.AddScoped<IToDoMasterService, ToDoMasterService>();
        services.AddScoped<IToDoReminderService, ToDoReminderService>();
        return services;
    }
}