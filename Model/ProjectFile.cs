using SourceModel;

namespace Model
{
    public class ProjectFile : ReferenceObject
    {
        public ProjectFile(string id)
            : base(ObjectTypes.FILE, id)
        {
        }
    }
}