using System.Windows.Input;
using DbSpy.Infrastructure.Commands;

namespace DbSpy.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _userName;
        private string _password;

        public LoginViewModel(LoginCommand loginCommand) 
                              //LoginCancelCommand loginCancelCommand)
        {
            LoginCommand = loginCommand;
            //LoginCancelCommand = loginCancelCommand;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                    return;
                _userName = value;
                OnPropertyChanged("UserName");
                //LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged("Password");
                //LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public BaseCommand LoginCommand { get; private set; }
        //public BaseCommand LoginCancelCommand { get; private set; }
    }
}
