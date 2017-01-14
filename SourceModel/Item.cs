namespace SourceModel
{
    public class Item
    {
        public Item(string referencingObjectName,
                    string referencingObjectType,
                    string referencedObjectName,
                    string referencedObjectType)
        {
            ReferencingObjectName = referencingObjectName;
            ReferencingObjectType = referencingObjectType;
            ReferencedObjectName = referencedObjectName;
            ReferencedObjectType = referencedObjectType;
        }

        public string ReferencingObjectName { get; private set; }
        public string ReferencingObjectType { get; private set; }
        public string ReferencedObjectName { get; private set; }
        public string ReferencedObjectType { get; private set; }
    }
}