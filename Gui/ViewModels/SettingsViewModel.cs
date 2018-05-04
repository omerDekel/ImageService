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
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RemoveCommand
        {
            get;
            set;
        }
        // clientmemeber 
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

        private void OnPropertyChanged(string v)
        {
            /**/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
