using System.IO;
using System.Xml.Serialization;
using Model;

namespace DbSpyServices
{
    public class DbSpySerializerService : IDbSpySerializerService
    {
        public void Serialize(ReferenceObject referanceObject, string fileName)
        {
            var streamWriter = new StreamWriter(fileName);
            var xmlSerializer = new XmlSerializer(referanceObject.GetType());
            xmlSerializer.Serialize(streamWriter, referanceObject);
        }
    }
}