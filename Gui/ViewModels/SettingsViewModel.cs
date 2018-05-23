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
        private Client guiClient;
        private string chosenDir;
        private ISettingsModel settingsModel;
        /// <summary>
        /// constructor 
        /// </summary>
        public SettingsViewModel ()
        {
            guiClient = new Client();
            settingsModel = new SettingsModel();
            settingsModel.PropertyChanged += OnPropertyChanged;
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            //ViewMOutputDirectory = "hiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RemoveCommand
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> DirectoryHandlers
        {
            get
            {
                return settingsModel.DirectoryHandlers;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ViewMOutputDirectory
        {
            get
            {
                return settingsModel.OutputDirectory;
            }
            set {; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SourceName
        {
            get
            {
                return settingsModel.SourceName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogName
        {
            get
            {
                return settingsModel.LogName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailSize
        {
            get
            {
                return settingsModel.ThumbnailSize;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChosenDir
        {
            get
            {
                return chosenDir;
            }
            set
            {
                chosenDir = value;
                //OnPropertyChanged("chosenDir");
                var command = RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /**/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        private void OnRemove(Object obj)
        {
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,null,"");
            guiClient.CommandToServer(command);
            //sending command to the imageservice client to close the directory handler
            settingsModel.DirectoryHandlers.Remove(chosenDir);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
