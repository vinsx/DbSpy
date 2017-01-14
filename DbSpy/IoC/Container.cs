using DbSpy.Infrastructure.Commands;
using DbSpy.Infrastructure.Contexts;
using DbSpy.ViewModels;
using DbSpy.Views;
using DbSpyServices;
using Microsoft.Practices.Unity;
using Repository;
using RepositoryInterface;

namespace DbSpy.IoC
{
    public static class Container
    {
        private static readonly UnityContainer Instance = new UnityContainer();

        public static T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return Instance.Resolve<T>(name);
        }

        public static void Dispose()
        {
            Instance.Dispose();
        }

        static Container()
        {
            //Repositories
            Instance.RegisterType<IDbRepository, DbRepository>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IFileRepository, FileRepository>(new ContainerControlledLifetimeManager());
            //Services 
            Instance.RegisterType<IObjectReferenceService, ObjectReferenceService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IDbSpySerializerService, DbSpySerializerService>(new ContainerControlledLifetimeManager());
            //View Models 
            Instance.RegisterType<LoginViewModel>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ShellCommandsViewModel>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<DbObjectRelationshipsViewModel>(new ContainerControlledLifetimeManager());
            //Views 
            Instance.RegisterType<IShellView, MainWindow>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IReferencingView, ReferencingTreeView>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ILoginView, LoginView>(new ContainerControlledLifetimeManager());
            //Commands 
            Instance.RegisterType<LoginCommand>(new ContainerControlledLifetimeManager());
            //Instance.RegisterType<LoginCancelCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ShowLoginCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<LoadDbRelationshipsCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<SaveDbRelationshipsCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ClearFilterCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<OpenFileInVisualStudioCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<CopySelectedDbObjectNameCommand>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<CopySelectedFileNameCommand>(new ContainerControlledLifetimeManager());
            //Contexts 
            Instance.RegisterType<ILoginContext, LoginContext>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IAppContext, AppContext>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ICommandContext, CommandContext>(new ContainerControlledLifetimeManager());
        }
    }
}
