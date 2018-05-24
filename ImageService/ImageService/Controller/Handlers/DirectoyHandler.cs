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
        /// <summary>
        /// constructor .
        /// </summary>
        /// <param name="m_controller"> Image Processing Controller </param>
        /// <param name="m_logging"></param>
        /// <param name="m_path">The Path of directory</param>
        public DirectoyHandler(IImageController m_controller, ILoggingService m_logging, string m_path)
        {
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            this.m_path = m_path;
            m_dirWatcher = new FileSystemWatcher(m_path);
            m_logging.Log("Created new DirectoryHandler", MessageTypeEnum.INFO);
        }
        /// <summary>
        /// OnCommandRecieved. The Event that will be activated upon new Command.
        /// </summary>
        /// <param name="sender"> sender of the event</param>
        /// <param name="e"> argument of the event</param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            if (sender == this) {
                this.m_logging.Log("a add commnad was recived",MessageTypeEnum.INFO);
            }
            else{
                this.m_logging.Log("a close commnad was recived", MessageTypeEnum.INFO);
            }

            bool result;
            // if the command is relevant to our directory (my directory or all the directorys).
            if (m_path.Equals(e.RequestDirPath) || e.RequestDirPath.Equals("*"))
            {
                this.m_logging.Log("relevant to the directory" + this.m_path, MessageTypeEnum.INFO);
                //if it's clossing directory command
                if (e.CommandID == (int)CommandEnum.CloseCommand)
                {
                    CloseHandle();
                }  else  {
                    string msg = m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
                    //here we need to check the result and send to the logger
                    if (result)
                    {
                        m_logging.Log(msg, MessageTypeEnum.INFO);
                    }
                    else
                    {
                        m_logging.Log(msg, MessageTypeEnum.FAIL);
                    }
                }
            }
        }
        /// <summary>
        /// OnCreated. The Event that will be activated upon new file got to the directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string[] fullPath = { e.FullPath };
            string pathExtension = Path.GetExtension(e.FullPath);
            m_logging.Log("OnCreated" + m_path, MessageTypeEnum.INFO);
            if (pathExtension.Equals(".jpg") || pathExtension.Equals(".png") || pathExtension.Equals(".gif") || pathExtension.Equals(".bmp")) {
                CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs(0, fullPath , m_path);
                OnCommandRecieved(this, commandRecievedEventArgs);
            }
            m_logging.Log("Created new File" + e.FullPath ,MessageTypeEnum.INFO);
        }
        /// <summary>
        /// StartHandleDirectory. The Function Recieves the directory to Handle .
        /// </summary>
        /// <param name="dirPath">the directory to Handle </param>
        public void StartHandleDirectory(string dirPath)
        {
            m_dirWatcher.Created += new FileSystemEventHandler(OnCreated);
            // begin watching .
            m_dirWatcher.EnableRaisingEvents = true;
            m_logging.Log("Start to handle directory"+dirPath,MessageTypeEnum.INFO);
        }
        /// <summary>
        /// close this directory handler close FileSystemWatcher  and invoke the DirectoryClose event .
        /// </summary>
        public void CloseHandle()
        {
            try
            {
                m_dirWatcher.EnableRaisingEvents = false;
                m_dirWatcher.Created -= new FileSystemEventHandler(OnCreated);
                DirectoryCloseEventArgs closeEventArgs = new DirectoryCloseEventArgs(m_path, "closing directory"+m_path);
                // we invoke to the server that it will know this directory was clossed: CommandRecieved -= this.OnCommandRecieved;
                DirectoryClose?.Invoke(this, closeEventArgs);
            } catch (Exception e){
                m_logging.Log("couldnt close handler" + m_path + " " + e.Message, MessageTypeEnum.FAIL);


            }
        }
    }
}
