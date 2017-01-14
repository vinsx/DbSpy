using System.Windows;
using System.Windows.Controls;
using DbSpy.IoC;
using DbSpy.ViewModels;
using Model;

namespace DbSpy.Views
{
    /// <summary>
    /// Interaction logic for ReferencingTreeView.xaml
    /// </summary>
    public partial class ReferencingTreeView : UserControl, IReferencingView
    {
        public ReferencingTreeView()
        {
            InitializeComponent();
            DataContext = Container.Resolve<DbObjectRelationshipsViewModel>();
        }

        public DbObjectRelationshipsViewModel DbObjectRelationshipsViewModel
        {
            get { return DataContext as DbObjectRelationshipsViewModel; }
        }

        private void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DbObjectRelationshipsViewModel.SelectedReferanceObject = e.NewValue as ReferenceObject;
        }

        private void OnSelectedProjectFileChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DbObjectRelationshipsViewModel.SelectedProjectFile = e.NewValue as ReferenceObject;
        }
    }
}
