using System.ComponentModel;
using DbSpy.Infrastructure.Contexts;
using DbSpy.Infrastructure.Extensions;
using Microsoft.Practices.Unity;

namespace DbSpy.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        [Dependency]
        public ILoginContext LoginContext { get; set; }

        [Dependency]
        public IAppContext AppContext { get; set; }

        [Dependency]
        public ICommandContext CommandContext { get; set; }

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Public Methods

        public T Resolve<T>()
        {
            return IoC.Container.Resolve<T>();
        }

        public virtual void Initialize() { }

        #endregion Public Methods

        #region Methods

        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Raise property changed event
            PropertyChanged.Raise(this, propertyName);
            //TODO: THis is not optimal!
            CommandContext.RaiseCanExecuteChanged();
        }

        #endregion Methods
    }
}
