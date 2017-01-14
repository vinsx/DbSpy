using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DbSpy.Infrastructure.Contexts;
using DbSpy.Infrastructure.Extensions;
using DbSpyServices;
using Model;

namespace DbSpy.ViewModels
{
    public class DbObjectRelationshipsViewModel : BaseViewModel
    {
        private readonly IObjectReferenceService _objectReferenceService;
        //private Database _database;
        private ObjectType _selectObjectType;
        private ObjectType? _selectFilterType;
        private ReferenceObject _selectedReferanceObject;
        private ReferenceObject _selectedProjectFile;
        private string _selectFilterObject;
        private string _selectedObjectSql;

        public DbObjectRelationshipsViewModel(IObjectReferenceService objectReferenceService)
        {
            _objectReferenceService = objectReferenceService;
            ObjectTypes = GetObjectTypes();
            FilterTypes = GetObjectTypes();
            _selectObjectType = ObjectType.StoredProcedure;
        }

        private static List<ObjectType> GetObjectTypes()
        {
            return new List<ObjectType>
            {
                ObjectType.StoredProcedure,
                ObjectType.View,
                ObjectType.Table
            };
        }

        public void Initializ()
        {
            OnPropertyChanged("Database");
            OnPropertyChanged("ReferanceObjectList");
            OnPropertyChanged("ReferancingFiles");
        }

        public Database Database
        {
            get
            {
                return AppContext.Database;
            }
        }

        public List<ObjectType> ObjectTypes { get; private set; }
        public List<ObjectType> FilterTypes { get; private set; }

        public ObjectType SelectObjectType
        {
            get { return _selectObjectType; }
            set
            {
                if (_selectObjectType == value)
                    return;
                _selectObjectType = value;
                OnPropertyChanged("SelectObjectType");
                OnPropertyChanged("ReferanceObjectList");
            }
        }

        public ObjectType? SelectFilterType
        {
            get { return _selectFilterType; }
            set
            {
                if (_selectFilterType == value)
                    return;
                if (_selectFilterType == null)
                    SelectFilterObject = null;
                _selectFilterType = value;
                OnPropertyChanged("SelectFilterType");
                OnPropertyChanged("FilterObjects");
            }
        }

        public IEnumerable<string> FilterObjects
        {
            get
            {
                return Database == null
                    ? null
                    : Database.Referances
                        .Where(r => r.ObjectType == SelectFilterType)
                        .OrderBy(r => r.Id)
                        .Select(r => r.Id);
            }
        }

        public string SelectFilterObject
        {
            get { return _selectFilterObject; }
            set
            {
                if (_selectFilterObject == value)
                    return;
                _selectFilterObject = value;
                OnPropertyChanged("SelectFilterObject");
                OnPropertyChanged("ReferanceObjectList");
            }
        }

        public IEnumerable<ReferenceObject> ReferanceObjectList
        {
            get 
            { 
                if(Database == null)
                    return null;
                               
                var items = Database.Referances.Where(r => r.ObjectType == SelectObjectType).OrderBy(r => r.Id);

                return SelectFilterObject.IsEmpty() ? items : items.Where(i => i.Referances.Any(r => r.Id == SelectFilterObject));
            }
        }

        public ReferenceObject SelectedReferanceObject
        {
            get { return _selectedReferanceObject; }
            set
            {
                if (_selectedReferanceObject == value)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;
                _selectedObjectSql = null;
                _selectedReferanceObject = value;
                if (_selectedReferanceObject != null)
                    _objectReferenceService.AddReferencingFiles(AppContext.Database, _selectedReferanceObject);
                //Initializ();
                OnPropertyChanged("SelectedReferanceObject");
                OnPropertyChanged("ReferancingFiles");
                GetDbObjectSql();
                //OnPropertyChanged("SelectedObjectSql");
                Mouse.OverrideCursor = null;
            }
        }

        public string SelectedObjectSql
        {
            get
            {
                if(!_selectedObjectSql.IsEmpty())
                    return _selectedObjectSql;
                if (SelectedReferanceObject == null)
                    _selectedObjectSql = null;
                //GetDbObjectSql();
                return _selectedObjectSql;
            }
        }

        readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

        private async Task GetDbObjectSql()
        {
            await _mutex.WaitAsync().ConfigureAwait(false);

            try
            {
                _selectedObjectSql = await _objectReferenceService.GetDbObjectSqlAsync(SelectedReferanceObject);
            }
            finally
            {
                _mutex.Release();
                OnPropertyChanged("SelectedObjectSql");
            }
        }

        public IEnumerable<ReferenceObject> ReferancingFiles
        {
            get
            {
                return Database == null || SelectedReferanceObject == null ? null
                                        : Database.ReferancingProjectFiles
                                                  .Where(r => r.Referances != null && r.Referances.Any(p => p.Id == SelectedReferanceObject.Id));
            }
        }

        public ReferenceObject SelectedProjectFile
        {
            get { return _selectedProjectFile; }
            set
            {
                if (_selectedProjectFile == value)
                    return;
                _selectedProjectFile = value;
                OnPropertyChanged("SelectedProjectFile");
            }
        }

        public void ClearFilter()
        {
            SelectFilterType = null;
        }
    }
}
