using System.Collections.Generic;

namespace RepositoryInterface
{
    public interface IFileRepository
    {
        List<string> GetReferencinFiles(string objectName);
    }
}