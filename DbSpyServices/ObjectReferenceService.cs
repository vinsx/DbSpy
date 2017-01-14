using System.Linq;
using System.Threading.Tasks;
using Model;
using RepositoryInterface;
using SourceModel;

namespace DbSpyServices
{
    public class ObjectReferenceService : IObjectReferenceService
    {
        private readonly IDbRepository _dbRepository;
        private readonly IFileRepository _fileRepository;

        public ObjectReferenceService(IDbRepository dbRepository,
                                      IFileRepository fileRepository)
        {
            _dbRepository = dbRepository;
            _fileRepository = fileRepository;
        }

        public Database GetDatabase()
        {
            var database = new Database();
            var items = _dbRepository.GetSource();
            //Get referenced and referencing objects from the database
            items.ForEach(i => ParseDbObjectReferences(database, i));
            ////Get referencing code files (For now look at stored procedures and views referenced from code.
            //database.Referances.Where(r => r.ObjectType == ObjectType.StoredProcedure || r.ObjectType == ObjectType.View)
            //                   .ToList()
            //                   .ForEach(r => AddReferencingFiles(database, r));

            return database;
        }

        public void AddReferencingFiles(Database database, ReferenceObject referencedObject)
        {
            if (referencedObject == null || referencedObject.FileReferancesLoaded)
                return;

            var referencingFiles = _fileRepository.GetReferencinFiles(referencedObject.Id);
            // Indicate that we already searched for this item.
            referencedObject.FileReferancesLoaded = true;

            if (referencingFiles.IsEmpty())
                return;

            referencingFiles.ForEach(f => AddReferencingFile(database, referencedObject, f));
        }

        public string GetDbObjectSql(ReferenceObject referanceObject)
        {
            if (referanceObject == null)
                return string.Empty;
            return _dbRepository.GetDbObjectSql(referanceObject.ObjectType.ToObjectTypeString(), referanceObject.Id);
        }

        public async Task<string> GetDbObjectSqlAsync(ReferenceObject referanceObject)
        {
            if (referanceObject == null)
                return string.Empty;
            return await _dbRepository.GetDbObjectSqlAsync(referanceObject.ObjectType.ToObjectTypeString(), referanceObject.Id);
        }

        private static void AddReferencingFile(Database database, ReferenceObject referencedObject, string fileName)
        {
            var dbObject = database.Referances.Where(r => r.ObjectType == ObjectType.ProjectFile).FirstOrDefault(f => f.Id == fileName);
            if (dbObject == null)
            {
                dbObject = new ReferenceObject(ObjectTypes.FILE, fileName);
                database.Referances.Add(dbObject);
            }
            if (!dbObject.Referances.Contains(referencedObject))
                dbObject.Referances.Add(referencedObject);
        }

        private static void ParseDbObjectReferences(Database database, Item item)
        {
            var referencedObject = AddDbObject(database, item.ReferencedObjectType, item.ReferencedObjectName);
            var referencingObject = AddDbObject(database, item.ReferencingObjectType, item.ReferencingObjectName);
            // Add to the referancing object.
            referencingObject.Referances.Add(referencedObject);
        }

        private static ReferenceObject AddDbObject(Database database, string objectType, string objectId)
        {
            var dbObject = database.Referances.FirstOrDefault(objectId);
            if (dbObject != null)
                return dbObject;

            dbObject = new ReferenceObject(objectType, objectId);

            database.Referances.Add(dbObject);

            return dbObject;
        }
    }
}
