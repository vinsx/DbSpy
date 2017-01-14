namespace DbSpy.Views
{
    public interface IShellView
    {
        void Show();
        void AddView(IReferencingView view);
    }
}
