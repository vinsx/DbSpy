using DbSpy.Views;

namespace DbSpy.Infrastructure.Commands
{
    public class ShowLoginCommand : BaseCommand
    {
        private readonly ILoginView _loginView;

        public ShowLoginCommand(ILoginView loginView)
        {
            _loginView = loginView;
        }

        public override bool CanExecute(object parameter)
        {
            return !LoginContext.IsLoggedIn;
        }

        public override void Execute(object parameter)
        {
            _loginView.ShowDialog();
        }
    }
}