using thinkbridge.Grp2BackendAN.DataAccess.Interfaces;

namespace thinkbridge.Grp2BackendAN.UnitTests
{
    #region testBase
    public class TestBase : IClassFixture<SetupFixture>
    {

    }

    public class TestBase<T> : TestBase, IDisposable
    {
        private IServiceScope _scope;
        private T _service;
        protected AppDbContext applicationDbContext { get; }

        protected T Service
        {
            get
            {
                if (_service == null)
                {
                    _service = GetService<T>();
                }
                return _service;
            }
        }

        public TestBase(SetupFixture setupFixture)
        {
            _scope = setupFixture.ServiceProvider.CreateScope();

            this.applicationDbContext = (AppDbContext)GetService<IAppDbContext>();
        }
        public void Dispose()
        {
        }

        protected T GetService<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }
    }

    #endregion


    #region testBaseCollection for parallelism

    [CollectionDefinition("TestBase Collation")]
    public class TestBaseCollationClass : ICollectionFixture<SetupFixture>
    {
    }

    [Collection("TestBase Collation")]
    public abstract class TestBaseCollection<T> : IDisposable
    {
        private IServiceScope _scope;
        private T _service;
        protected AppDbContext applicationDbContext { get; }
        protected T Service
        {
            get
            {
                if (_service == null)
                {
                    _service = GetService<T>();
                }
                return _service;
            }
        }
        public TestBaseCollection(SetupFixture setupFixture)
        {
            _scope = setupFixture.ServiceProvider.CreateScope();
            this.applicationDbContext = (AppDbContext)GetService<IAppDbContext>();
        }
        public void Dispose()
        {
        }
        protected T GetService<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }
    }
    #endregion
}
