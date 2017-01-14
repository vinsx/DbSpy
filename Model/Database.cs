using System;
using SourceModel;

namespace Model
{
    [Serializable]
    public class Database : ReferenceObject
    {
        public Database(string id = "Database")
            : base(ObjectTypes.DATABASE, id)
        {
        }
    }
}
