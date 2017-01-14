using Model;

namespace DbSpyServices
{
    public interface IDbSpySerializerService
    {
        void Serialize(ReferenceObject referanceObject, string fileName);
    }
}
