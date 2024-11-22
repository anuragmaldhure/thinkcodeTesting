
namespace thinkbridge.Grp2BackendAN.UnitTests
{
    public class SetupFixture : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public SetupFixture()
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "../../../");
            // Setup the configuration
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Setup the DI
            var services = new ServiceCollection();

            services.AddOptions();
            services.AddLogging(o =>
            {
                o.ClearProviders();
                o.AddDebug();
            });

            services.AddScoped<IConfiguration>((x) => { return _configuration; });
            services.AddApplicationLayer(_configuration);
            services.AddHttpContextAccessor();
            services.AddCoreLayer();
            services.AddApiLayer();
            services.AddDataAccessLayer(_configuration);
            _serviceProvider = services.BuildServiceProvider();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                var pendingMigrations = context.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void Dispose()
        {
        }

        public IServiceProvider ServiceProvider { get { return _serviceProvider; } }
        public IConfiguration Configuration { get { return _configuration; } }
    }
}
