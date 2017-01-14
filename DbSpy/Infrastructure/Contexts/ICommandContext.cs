namespace DbSpy.Infrastructure.Contexts
{
    public interface ICommandContext
    {
        void RaiseCanExecuteChanged();
    }
}
