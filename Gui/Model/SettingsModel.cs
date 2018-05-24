using Gui.Comunication;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

        public SettingsModel()
        {
            clientGui = Client.Instance;
            ClientGui.CommandRecieved += OnCommandRecieved;
            clientGui.CommandFromServer();
            //taking the config arguments by the GetConfigCommand
            DirectoryHandlers = new ObservableCollection<string>();
            string[] arguments = new string[5];
            CommandRecievedEventArgs getConfigCommand = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arguments, "");
            ClientGui.CommandToServer(getConfigCommand);
        }

        public ObservableCollection<string> DirectoryHandlers { get; set; }
        public string OutputDirectory
        {
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

        public IClient ClientGui
        {
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
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            try
            {
                if (e.CommandID == (int)CommandEnum.GetConfigCommand)
                {
                    this.outputDirectory = e.Args[0];
                    this.sourceName = e.Args[1];
                    logName = e.Args[2];
                    thumbnailSize = e.Args[3];
                    string[] directoryArray = e.Args[4].Split(';');
                    for (int i = 0; i < directoryArray.Length; i++)
                    {
                        string deleteDir = directoryArray[i];
                        if (deleteDir != null && DirectoryHandlers != null && DirectoryHandlers.Contains(deleteDir))
                        {
                            DirectoryHandlers.Add(deleteDir);

                        }
                    }
                }
                else if (e.CommandID == (int)CommandEnum.CloseCommand)
                {
                    this.DirectoryHandlers.Remove(e.Args[0]);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
