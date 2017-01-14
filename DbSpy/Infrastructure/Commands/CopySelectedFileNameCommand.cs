using System.Windows;
using DbSpy.ViewModels;

namespace DbSpy.Infrastructure.Commands
{
    public class CopySelectedFileNameCommand : BaseCommand
    {
        private readonly DbObjectRelationshipsViewModel _dbObjectRelationshipsViewModel;

        public CopySelectedFileNameCommand(DbObjectRelationshipsViewModel dbObjectRelationshipsViewModel)
        {
            _dbObjectRelationshipsViewModel = dbObjectRelationshipsViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return LoginContext.IsLoggedIn && _dbObjectRelationshipsViewModel.SelectedProjectFile != null;
        }

        public override void Execute(object parameter)
        {
            Clipboard.SetText(_dbObjectRelationshipsViewModel.SelectedProjectFile.Id);
        }
    }
}