using System.Windows;
using DbSpy.IoC;
using DbSpy.ViewModels;

namespace DbSpy.Views
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LoginView : Window, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = Container.Resolve<LoginViewModel>();
        }
    }
}
