using System.Windows;
using DbSpy.ViewModels;

namespace DbSpy.Infrastructure.Commands
{
    public class CopySelectedDbObjectNameCommand : BaseCommand
    {
        private readonly DbObjectRelationshipsViewModel _dbObjectRelationshipsViewModel;

        public CopySelectedDbObjectNameCommand(DbObjectRelationshipsViewModel dbObjectRelationshipsViewModel)
        {
            _dbObjectRelationshipsViewModel = dbObjectRelationshipsViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return LoginContext.IsLoggedIn && _dbObjectRelationshipsViewModel.SelectedReferanceObject != null;
        }

        public override void Execute(object parameter)
        {
            Clipboard.SetText(_dbObjectRelationshipsViewModel.SelectedReferanceObject.Id);
        }
    }
}
