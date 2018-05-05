using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gui.Model;
using Microsoft.Practices.Prism.Commands;

namespace Gui.ViewModels
{
    class SettingsViewModel : ISettingsViewModel
    {
        private string chosenDir;
        private ISettingsModel settingsModel;
        /// <summary>
        /// constructor 
        /// </summary>
        public SettingsViewModel ()
        {
            this.settingsModel = new SettingsModel();
            settingsModel.PropertyChanged += OnPropertyChanged;
            this.RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RemoveCommand
        // todo: clientmemeber 
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
                return this.chosenDir;
            }
            set
            {
                chosenDir = value;
                //OnPropertyChanged("chosenDir");
                var command = this.RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /**/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        private void OnRemove(Object obj)
        {
            //sending command to the imageservice client to close the directory handler
            this.DirectoryHandlers.Remove(chosenDir);
        }
        private bool CanRemove(object obj)
        {
            if (chosenDir == null)
            {
                return false;
            }
            return true;
        }


    }

}
