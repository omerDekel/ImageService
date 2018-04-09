using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public DirectoyHandler(IImageController m_controller, ILoggingService m_logging, FileSystemWatcher m_dirWatcher,
            string m_path)
        {
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            this.m_path = m_path;
            this.m_dirWatcher = new FileSystemWatcher(m_path);
        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            // if the command is relevant to our directory
            if (m_path.Equals(e.Args[0]))
            {
                string msg = m_controller.ExecuteCommand(e.CommandID, e.Args, out result);

                //here we need to check the result and send to the logger
                if (result)
                {           
                    this.m_logging.Log(msg, MessageTypeEnum.INFO);
                } else
                {
                    this.m_logging.Log(msg, MessageTypeEnum.FAIL);
                }

            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string[] fullPath = { e.FullPath }; 
            string pathExtension = Path.GetExtension(e.FullPath);
            if (pathExtension.Equals(".jpg") || pathExtension.Equals(".png") || pathExtension.Equals(".gif") || pathExtension.Equals(".bmp")) {
                CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs(0,fullPath , m_path);
                OnCommandRecieved(this, commandRecievedEventArgs);
            }
        }

        public void StartHandleDirectory(string dirPath)
        {
          
            m_dirWatcher.Changed += new FileSystemEventHandler(OnCreated);
            m_dirWatcher.Created += new FileSystemEventHandler(OnCreated);
            // begin watching .
            m_dirWatcher.EnableRaisingEvents = true;
        }

    }
}
