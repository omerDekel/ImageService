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
<<<<<<< HEAD
        private IClient clientGui;
        //public IClient ClientGui { get; set; }
        public SettingsModel() {
            clientGui = Client.Instance;
            clientGui.CommandFromServer();
=======
        public SettingsModel() {
>>>>>>> parent of e3ced37... HEYUSH
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
<<<<<<< HEAD
<<<<<<< HEAD
        public ObservableCollection<string> DirectoryHandlers { get; set; }
        public string OutputDirectory
        {
=======
        public IClient Client { get; set; }
        public ObservableCollection<string> DirectoryHandlers { get ; set ; }
        public string OutputDirectory {
>>>>>>> parent of e3ced37... HEYUSH
=======
        public ObservableCollection<string> DirectoryHandlers { get ; set ; }
        public string OutputDirectory {
>>>>>>> parent of 10b194c... Settings model  get commands from the server
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
<<<<<<< HEAD

        public IClient ClientGui {
            get
            {
                return this.clientGui;
            }
            
            set => throw new NotImplementedException();
        }

=======
>>>>>>> parent of e3ced37... HEYUSH
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(name));
        }
    }
}
