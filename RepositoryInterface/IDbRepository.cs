using System.Collections.Generic;
using System.Threading.Tasks;
using SourceModel;

namespace RepositoryInterface
{
    public interface IDbRepository
    {
        bool Login(string userName, string password);
        bool LoggOut();
        List<Item> GetSource();
        string GetDbObjectSql(string objectType, string objectName);
        Task<string> GetDbObjectSqlAsync(string objectType, string objectName);
    }
}
