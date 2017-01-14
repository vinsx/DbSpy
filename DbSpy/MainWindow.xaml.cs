using System.ComponentModel;
using System.Windows;
using DbSpy.ViewModels;
using DbSpy.Views;
using Container = DbSpy.IoC.Container;

namespace DbSpy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IShellView
    {
        public MainWindow()
        {
            InitializeComponent();
            AddView(Container.Resolve<IReferencingView>());
            DataContext = Container.Resolve<ShellCommandsViewModel>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Container.Dispose();
            base.OnClosing(e);
        }

        public void AddView(IReferencingView view)
        {
            ContentControl.Content = view;
        }
    }
}
