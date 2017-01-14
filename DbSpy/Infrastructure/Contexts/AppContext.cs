using Model;

namespace DbSpy.Infrastructure.Contexts
{
    public class AppContext : IAppContext
    {
        public AppContext()
        {
            
        }

        public Database Database { get; set; }
    }
}