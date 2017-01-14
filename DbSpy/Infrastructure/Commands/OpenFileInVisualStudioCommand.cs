using System.Diagnostics;
using DbSpy.ViewModels;

namespace DbSpy.Infrastructure.Commands
{
    public class OpenFileInVisualStudioCommand : BaseCommand
    {
        private readonly DbObjectRelationshipsViewModel _dbObjectRelationshipsViewModel;

        public OpenFileInVisualStudioCommand(DbObjectRelationshipsViewModel dbObjectRelationshipsViewModel)
        {
            _dbObjectRelationshipsViewModel = dbObjectRelationshipsViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return _dbObjectRelationshipsViewModel.SelectedProjectFile != null;
        }

        public override void Execute(object parameter)
        {
            if(_dbObjectRelationshipsViewModel.SelectedProjectFile == null)
                return;

            var process = new Process();
            var startInfo = new ProcessStartInfo("devenv.exe", string.Format("/edit {0}", _dbObjectRelationshipsViewModel.SelectedProjectFile.Id));
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
