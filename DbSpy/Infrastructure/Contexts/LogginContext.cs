using RepositoryInterface;

namespace DbSpy.Infrastructure.Contexts
{
    public class LoginContext : ILoginContext
    {
        private readonly IDbRepository _dbRepository;

        public LoginContext(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public bool IsLoggedIn { get; private set; }

        public void Login(string userName, string password)
        {
            IsLoggedIn = _dbRepository.Login(userName, password);
        }

        public void LoggOut()
        {
            IsLoggedIn = !_dbRepository.LoggOut();
        }
    }
}
