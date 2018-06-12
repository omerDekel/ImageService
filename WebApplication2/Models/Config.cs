using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Comunication;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using ImageService.Infrastructure;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.ComponentModel;

namespace WebApplication2.Models
{
    /// <summary>
    /// model handling logs and configurations.
    /// </summary>
    public class Config
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string ChosenDir { get; set; }
        private IClient clientGui;
        private static Config instance;
        // list of logs .
        public List<LogTypeAndMessage> logArr { get; }

        /// <summary>
        /// Constructor .
        /// </summary>
        private Config()
        {
            clientGui = Client.Instance;
            clientGui.CommandRecieved += OnCommandRecieved;
            this.logArr = new List<LogTypeAndMessage>();
            //taking the config arguments by the GetConfigCommand
            DirectoryHandlers = new ObservableCollection<string>();
            string[] arguments = new string[5];
            CommandRecievedEventArgs getConfigCommand = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arguments, "");
            clientGui.CommandToServer(getConfigCommand);
            // for the command of getting config .
            clientGui.OneCommandFromServer();

            clientGui.CommandFromServer();
        }
        /// <summary>
        /// instance for singleton
        /// </summary>
        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }
        /// <summary>
        /// Delete Directory Handler .
        /// </summary>
        public void DeleteDirectoryHandler()
        {
            if (this.DirectoryHandlers.Contains(ChosenDir))
            {
                CommandRecievedEventArgs closeCommand = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, ChosenDir);
                clientGui.CommandToServer(closeCommand);
            }
        }
        /// <summary>
        /// on command recieved event handler function .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            try
            {
                //if it's command of get config .
                if (e.CommandID == (int)CommandEnum.GetConfigCommand)
                {
                    OutputDirectory = e.Args[0];
                    Console.Write("OUTPUT DIRECTORY IS:");
                    Console.WriteLine(e.Args[0]);
                    SourceName = e.Args[1];
                    LogName = e.Args[2];
                    ThumbnailSize = e.Args[3];
                    string[] directoryArray = e.Args[4].Split(';');
                    for (int i = 0; i < directoryArray.Length; i++)
                    {
                        string dir = directoryArray[i];
                        Console.WriteLine("dir name" + dir);
                        Console.WriteLine("dir was added " + dir);
                        DirectoryHandlers.Add(dir);
                    }
                }
                //if it's event of close directory command .
                else if (e.CommandID == (int)CommandEnum.CloseCommand)
                {
                    if (e.RequestDirPath != null && DirectoryHandlers != null && DirectoryHandlers.Contains(e.RequestDirPath))
                    {

                        this.DirectoryHandlers.Remove(e.RequestDirPath);
                        PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(e.RequestDirPath));
                    }
                }
                // if it's get log command
                else if (e.CommandID == (int)CommandEnum.LogCommand)
                {
                    // translate the Args array of CommandRecievedEventArgs to logs and add them to logCollection
                    for (int i = 0; i < e.Args.Length; i += 2)
                    {
                        LogTypeAndMessage log = new LogTypeAndMessage();
                        log.Type = e.Args[i];
                        log.Message = e.Args[i + 1];
                        this.logArr.Add(log);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        [Required]
        [DataType(DataType.Text)]
        public bool Enabled { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DirectoryHandlers")]
        public ObservableCollection<string> DirectoryHandlers { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDirectory")]
        public string OutputDirectory { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize")]
        public string ThumbnailSize { get; set; }

        [Required]
        [Display(Name = "LogArr ")]
        public List<LogTypeAndMessage> LogAr { get; }
    }
}