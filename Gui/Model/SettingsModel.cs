using Gui.Comunication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Model
{
    class SettingsModel : ISettingsModel
    {
        private String outputDirectory;
        private String sourceName;
        private String logName;
        private String thumbnailSize;
        private IClient clientGui;
        //public IClient ClientGui { get; set; }
        public SettingsModel() {
            clientGui = Client.Instance;
            clientGui.CommandFromServer();
            //todo: taking the config arguments by the GetConfigCommand
            this.outputDirectory = "OutputDirectory";
            this.sourceName = "SourceName";
            logName = "LogName";
            thumbnailSize = "ThumbnailSize";
            DirectoryHandlers = new ObservableCollection<string>();
            DirectoryHandlers.Add("hi");
            DirectoryHandlers.Add("shalom");
            DirectoryHandlers.Add("hishuv");
        }
        public ObservableCollection<string> DirectoryHandlers { get ; set ; }
        public string OutputDirectory {
            get
            {
                return outputDirectory;
            }
            set
            {
                outputDirectory = value;
                OnPropertyChanged("OutputDirectory");
            }
        }
        public string SourceName
        {
            get
            {
                return sourceName;
            }
            set
            {
                sourceName = value;
                OnPropertyChanged("SourceName");
            }
        }

        public string LogName
        {
            get
            {
                return logName;
            }
            set
            {
                logName = value;
                OnPropertyChanged("LogName");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailSize
        {
            get
            {
                return thumbnailSize;
            }
            set
            {
                thumbnailSize = value;
                OnPropertyChanged("ThumbnailSize");
            }
        }

        public IClient ClientGui {
            get
            {
                return this.clientGui;
            }
            
            set => throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(name));
        }
    }
}
