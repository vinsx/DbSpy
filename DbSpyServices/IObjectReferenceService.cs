using System.Threading.Tasks;
using Model;

namespace DbSpyServices
{
    public interface IObjectReferenceService
    {
        Database GetDatabase();
        void AddReferencingFiles(Database database, ReferenceObject referencedObject);
        string GetDbObjectSql(ReferenceObject referance);
        Task<string> GetDbObjectSqlAsync(ReferenceObject referanceObject);
    }
}