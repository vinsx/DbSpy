using Model;

namespace DbSpy.Infrastructure.Contexts
{
    public interface IAppContext
    {
        Database Database { get; set; }
    }
}
