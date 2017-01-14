using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Model
{
    [Serializable]
    public class ReferenceObject
    {
        public ReferenceObject(string dbObjectType, string id)
        {
            ObjectType = dbObjectType.ToDbObjectType();
            Id = id;
            Referances = new List<ReferenceObject>();
            //ReferancingProjectFiles = new List<ProjectFile>();
            FileReferancesLoaded = false;
        }

        [XmlAttribute("Id")]
        public string Id { get; private set; }

        [XmlAttribute("ObjectType")]
        public ObjectType ObjectType { get; private set; }

        public bool FileReferancesLoaded { get; set; }

        //public IEnumerable<ReferenceObject> ReferancingProjectFiles { get; private set; }

        [XmlIgnore]
        public IEnumerable<ReferenceObject> ReferancingProjectFiles
        {
            get { return Referances.Where(x => x.ObjectType == ObjectType.ProjectFile); }
        }

        [XmlIgnore]
        public List<ReferenceObject> ReferanceObjectList
        {
            get
            {
                return Referances.Where(r => r.ObjectType == ObjectType.StoredProcedure ||
                                             r.ObjectType == ObjectType.View ||
                                             r.ObjectType == ObjectType.Table)
                                 .OrderBy(r => r.ObjectType)
                                 .ToList();
            }
        }

        [XmlIgnore]
        public List<ReferenceObject> Referances { get; private set; }

        public IEnumerable<ReferenceObject> Tables
        {
            get { return Referances.Where(x => x.ObjectType == ObjectType.Table); }
        }

        public IEnumerable<ReferenceObject> Views
        {
            get { return Referances.Where(x => x.ObjectType == ObjectType.View); }
        }

        public IEnumerable<ReferenceObject> StoredProcedures
        {
            get { return Referances.Where(x => x.ObjectType == ObjectType.StoredProcedure); }
        }

        public IEnumerable<ReferenceObject> Constraints
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.Constraint);
            }
        }

        public IEnumerable<ReferenceObject> InlineTableValuedFunctions
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.InlineTableValuedFunction);
            }
        }

        public IEnumerable<ReferenceObject> InvalidDbObjects
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.InvalidDbObject);
            }
        }

        public IEnumerable<ReferenceObject> ScalarFunctions
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.ScalarFunction);
            }
        }

        public IEnumerable<ReferenceObject> TableValuedFunctions
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.TableValuedFunction);
            }
        }

        public IEnumerable<ReferenceObject> Triggers
        {
            get
            {
                return Referances.Where(x => x.ObjectType == ObjectType.Trigger);
            }
        }


    }
}