using DbSpy.Infrastructure.Extensions;
using DbSpy.IoC;
using DbSpy.ViewModels;
using DbSpy.Views;

namespace DbSpy.Infrastructure.Commands
{
    public class LoginCommand : BaseCommand
    {
        public LoginViewModel LoginViewModel { get { return Container.Resolve<LoginViewModel>(); } }
        public ILoginView LogInView { get { return Container.Resolve<ILoginView>(); } }

        public override bool CanExecute(object parameter)
        {
            return !LoginContext.IsLoggedIn && !LoginViewModel.UserName.IsEmpty() && !LoginViewModel.Password.IsEmpty();
        }

        public override void Execute(object parameter)
        {
            LoginContext.Login(LoginViewModel.UserName, LoginViewModel.Password);
            LogInView.Close();
        }
    }
}