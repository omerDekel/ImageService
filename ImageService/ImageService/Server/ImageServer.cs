using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        public ImageServer(ILoggingService logger, IImageController controller, String[] directories)
        {
            m_logging = logger;
            m_controller = controller;
            DirectoyHandler directoyHandler;
            for( int i = 0; i < directories.Length; i++)
            {
                directoyHandler = new DirectoyHandler(m_controller, m_logging, directories[i]);
                CommandRecieved += directoyHandler.OnCommandRecieved;
                directoyHandler.DirectoryClose += OnDirectoryClose;
                directoyHandler.StartHandleDirectory(directories[i]);
                m_logging.Log("Created directory handler " + directories[i], Logging.Modal.MessageTypeEnum.INFO);
            }
            m_logging.Log("Created server", Logging.Modal.MessageTypeEnum.INFO);
        }
        public void OnDirectoryClose (object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                CommandRecieved -= ((DirectoyHandler)sender).OnCommandRecieved;
                m_logging.Log(e.Message, Logging.Modal.MessageTypeEnum.INFO);
            } catch
            {
                m_logging.Log("failed to close directory handler", Logging.Modal.MessageTypeEnum.FAIL);

            }
        }
        public void OnClosedService()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs(1,null, "*");
            CommandRecieved?.Invoke(this, commandRecievedEventArgs);
            m_logging.Log("Server notify to close directory handler", Logging.Modal.MessageTypeEnum.INFO);
        }
    }
}
