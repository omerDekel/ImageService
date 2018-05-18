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
        private IClient client;
        private String outputDirectory;
        private String sourceName;
        private String logName;
        private String thumbnailSize;
        public SettingsModel() {
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
        public IClient Client { get; set; }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(name));
        }
    }
}
