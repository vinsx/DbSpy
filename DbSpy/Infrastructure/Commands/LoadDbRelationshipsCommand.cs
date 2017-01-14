using System.Windows.Input;
using DbSpy.ViewModels;
using DbSpyServices;

namespace DbSpy.Infrastructure.Commands
{
    public class LoadDbRelationshipsCommand : BaseCommand
    {
        private readonly IObjectReferenceService _objectReferenceService;
        private readonly DbObjectRelationshipsViewModel _dbObjectRelationshipsViewModel;

        public LoadDbRelationshipsCommand(IObjectReferenceService objectReferenceService,
                                          DbObjectRelationshipsViewModel dbObjectRelationshipsViewModel)
        {
            _objectReferenceService = objectReferenceService;
            _dbObjectRelationshipsViewModel = dbObjectRelationshipsViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return LoginContext.IsLoggedIn;
        }

        public override void Execute(object parameter)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            AppContext.Database = _objectReferenceService.GetDatabase();
            _dbObjectRelationshipsViewModel.Initializ();// .Database = AppContext.Database;
            Mouse.OverrideCursor = null;
        }
    }
}