using DbSpy.IoC;
using DbSpy.ViewModels;

namespace DbSpy.Infrastructure.Contexts
{
    public class CommandContext : ICommandContext
    {
        public void RaiseCanExecuteChanged()
        {
            var commandViewModel = Container.Resolve<ShellCommandsViewModel>();
            commandViewModel.RaiseCanExecuteChanged();
        }
        
    }
}