using DbSpyServices;
using Microsoft.Win32;

namespace DbSpy.Infrastructure.Commands
{
    public class SaveDbRelationshipsCommand : BaseCommand
    {
        private readonly IDbSpySerializerService _dbSpySerializerService;

        public SaveDbRelationshipsCommand(IDbSpySerializerService dbSpySerializerService)
        {
            _dbSpySerializerService = dbSpySerializerService;
        }

        public override bool CanExecute(object parameter)
        {
            return LoginContext.IsLoggedIn && AppContext.Database != null;
        }

        public override void Execute(object parameter)
        {
            var  dialog = new SaveFileDialog
            {
                FileName = "DbObjectRelationships",
                DefaultExt = ".xml",
                Filter = "Text documents (.txt)|*.txt"
            };

            if (dialog.ShowDialog() == true)
            {
                _dbSpySerializerService.Serialize(AppContext.Database, dialog.FileName);
            }
        }
    }
}