namespace DbSpy.Infrastructure.Contexts
{
    public interface ILoginContext
    {
        bool IsLoggedIn { get; }
        void Login(string userName, string password);
        void LoggOut();
    }
}