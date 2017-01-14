using DbSpy.ViewModels;

namespace DbSpy.Infrastructure.Commands
{
    public class ClearFilterCommand : BaseCommand
    {
        private readonly DbObjectRelationshipsViewModel _dbObjectRelationshipsViewModel;

        public ClearFilterCommand(DbObjectRelationshipsViewModel dbObjectRelationshipsViewModel)
        {
            _dbObjectRelationshipsViewModel = dbObjectRelationshipsViewModel;
        }

        //public override bool CanExecute(object parameter)
        //{
        //    return _dbObjectRelationshipsViewModel.SelectedProjectFile != null;
        //}

        public override void Execute(object parameter)
        {
            _dbObjectRelationshipsViewModel.ClearFilter();
        }
    }
}
