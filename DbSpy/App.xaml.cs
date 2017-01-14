using System.Runtime.InteropServices;
using System.Windows;
using DbSpy.IoC;
using DbSpy.Views;

namespace DbSpy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            var shell = Container.Resolve<IShellView>();
            shell.Show();
        }
    }
}
