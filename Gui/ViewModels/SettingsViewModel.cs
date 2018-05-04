using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.ViewModels
{
    class SettingsViewModel : ISettingsViewModel
    {
        public ObservableCollection<string> DirectoryHandlers => throw new NotImplementedException();

        public string OutputDirectory => throw new NotImplementedException();

        public string SourceName => throw new NotImplementedException();

        public string LogName => throw new NotImplementedException();

        public string ThumbnailSize => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
        private string chosenDir;
        public string ChosenDir
        {
            get
            {
                return this.chosenDir;
            }
            set
            {
                chosenDir = value;
                OnPropertyChanged("chosenDir");
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }
    }
}
