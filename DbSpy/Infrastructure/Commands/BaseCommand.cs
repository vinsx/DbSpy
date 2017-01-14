using System;
using System.Windows.Input;
using DbSpy.Infrastructure.Contexts;
using Microsoft.Practices.Unity;

namespace DbSpy.Infrastructure.Commands
{
    public abstract class BaseCommand : ICommand
    {
        private event EventHandler _internalCanExecuteChanged;
        [Dependency]
        public ILoginContext LoginContext { get; set; }
        [Dependency]
        public IAppContext AppContext { get; set; }

        public virtual bool CanExecute(object parameter)
        {
            return LoginContext.IsLoggedIn;
        }

        public virtual void Execute(object parameter)
        {
            
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                _internalCanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _internalCanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            //if (canExecute != null)
                OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            //var canExecuteChanged = _internalCanExecuteChanged;
            if (_internalCanExecuteChanged != null)
                _internalCanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
