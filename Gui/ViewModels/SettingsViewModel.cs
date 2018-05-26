using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gui.Comunication;
using Gui.Model;
using ImageService.Modal;
using ImageService.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using ImageService.Infrastructure.Enums;

namespace Gui.ViewModels
{
    class SettingsViewModel : ISettingsViewModel
    {
        //private Client guiClient;
        private string chosenDir;
        private ISettingsModel settingsModel;
        /// <summary>
        /// constructor 
        /// </summary>
        public SettingsViewModel()
        {
            settingsModel = new SettingsModel();
            settingsModel.PropertyChanged += OnPropertyChanged;
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// remove directory command from the directory handler list.
        /// </summary>
        public ICommand RemoveCommand
        {
            get;
            set;
        }
        public ObservableCollection<string> DirectoryHandlers
        {
            get
            {
                return settingsModel.DirectoryHandlers;
            }
        }
        public string OutputDirectory
        {
            get
            {
                return settingsModel.OutputDirectory;
            }
            set {; }
        }
        public string SourceName
        {
            get
            {
                return settingsModel.SourceName;
            }
        }

        public string LogName
        {
            get
            {
                return settingsModel.LogName;
            }
        }
        public string ThumbnailSize
        {
            get
            {
                return settingsModel.ThumbnailSize;
            }
        }
        public string ChosenDir
        {
            get
            {
                return chosenDir;
            }
            set
            {
                // if directory raise remove command 
                chosenDir = value;
                var command = RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        private void OnRemove(Object obj)
        {
            //sending command to the imageservice client to close the directory handler
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, chosenDir);
            settingsModel.CommandToServer(command);
            settingsModel.RemoveDirectory(chosenDir);
        }
        /// <summary>
        /// make the on remove command possible if directory was chosen. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> true if directory can be reomved , else false</returns>
        private bool CanRemove(object obj)
        {
            if (ChosenDir == null)
            {
                return false;
            }
            return true;
        }


    }

}
