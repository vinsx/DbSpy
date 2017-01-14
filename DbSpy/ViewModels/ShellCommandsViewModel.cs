using System.Collections.Generic;
using System.Windows.Input;
using DbSpy.Infrastructure.Commands;
using Microsoft.Practices.ObjectBuilder2;

namespace DbSpy.ViewModels
{
    public class ShellCommandsViewModel : BaseViewModel
    {
        private readonly Dictionary<string, ICommand> _commands;

        public ShellCommandsViewModel(LoadDbRelationshipsCommand loadDbRelationshipsCommand,
                                      SaveDbRelationshipsCommand saveDbRelationshipsCommand,
                                      ShowLoginCommand showLoginCommand,
                                      ClearFilterCommand clearFilterCommand,
                                      OpenFileInVisualStudioCommand openFileInVisualStudioCommand,
                                      CopySelectedDbObjectNameCommand copySelectedDbObjectNameCommand,
                                      CopySelectedFileNameCommand copySelectedFileNameCommand)
        {
            _commands = new Dictionary<string, ICommand>();
            OpenFileInVisualStudioCommand = openFileInVisualStudioCommand;
            ShowLoginCommand = showLoginCommand;
            LoadDbRelationshipsCommand = loadDbRelationshipsCommand;
            SaveDbRelationshipsCommand = saveDbRelationshipsCommand;
            ClearFilterCommand = clearFilterCommand;
            CopySelectedDbObjectNameCommand = copySelectedDbObjectNameCommand;
            CopySelectedFileNameCommand = copySelectedFileNameCommand;
        }

        public ICommand LoadDbRelationshipsCommand
        {
            get { return _commands["LoadDbRelationshipsCommand"]; }
            private set { _commands["LoadDbRelationshipsCommand"] = value; }
        }

        public ICommand SaveDbRelationshipsCommand
        {
            get { return _commands["SaveDbRelationshipsCommand"]; }
            private set { _commands["SaveDbRelationshipsCommand"] = value; }
        }

        public ICommand ShowLoginCommand
        {
            get { return _commands["ShowLoginCommand"]; }
            private set { _commands["ShowLoginCommand"] = value; }
        }

        public ICommand ClearFilterCommand
        {
            get { return _commands["ClearFilterCommand"]; }
            private set { _commands["ClearFilterCommand"] = value; }
        }

        public ICommand OpenFileInVisualStudioCommand
        {
            get { return _commands["OpenFileInVisualStudioCommand"]; }
            private set { _commands["OpenFileInVisualStudioCommand"] = value; }
        }

        public ICommand CopySelectedDbObjectNameCommand
        {
            get { return _commands["CopySelectedDbObjectNameCommand"]; }
            private set { _commands["CopySelectedDbObjectNameCommand"] = value; }
        }

        public ICommand CopySelectedFileNameCommand
        {
            get { return _commands["CopySelectedFileNameCommand"]; }
            private set { _commands["CopySelectedFileNameCommand"] = value; }
        }

        public void RaiseCanExecuteChanged()
        {
            _commands.ForEach(c => ((BaseCommand)c.Value).RaiseCanExecuteChanged());
        }
    }
}