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

namespace WebApplication2.Models
{
    public class Config
    {
        
        /*private String outputDirectory;
        private String sourceName;
        private String logName;
        private String thumbnailSize;*/
        // the client we comunicate through it with the server .
       private IClient clientGui;
        /// <summary>
        /// Constructor .
        /// </summary>
        public Config()
        {
            /*clientGui = Client.Instance;
            clientGui.CommandRecieved += OnCommandRecieved;
            clientGui.CommandFromServer();*/
            //taking the config arguments by the GetConfigCommand
            DirectoryHandlers = new ObservableCollection<string>();
            /*string[] arguments = new string[5];
            CommandRecievedEventArgs getConfigCommand = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arguments, "");
            clientGui.CommandToServer(getConfigCommand);*/
        }
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            /*try
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
                       // App.Current.Dispatcher.Invoke((Action)delegate
                       // {
                            DirectoryHandlers.Add(dir);

                       // });
                    }
                }
                //if it's event of close directory command .
                else if (e.CommandID == (int)CommandEnum.CloseCommand)
                {
                    if (e.RequestDirPath != null && DirectoryHandlers != null && DirectoryHandlers.Contains(e.RequestDirPath))
                    {
                        //App.Current.Dispatcher.Invoke((Action)delegate
                       //// {
                            this.DirectoryHandlers.Remove(e.RequestDirPath);

                      //  });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }*/
        }
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

    }
}