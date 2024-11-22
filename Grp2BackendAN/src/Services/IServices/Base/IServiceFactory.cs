namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IServiceFactory
{
    IServiceScope CreateScope();
    T GetService<T>(IServiceScope scope);
}
