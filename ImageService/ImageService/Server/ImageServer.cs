using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    /// <summary>
    /// ImageServer . server that menages directory handlers .
    /// </summary>
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        private ComunicationServer m_commServer;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion
        /// <summary>
        /// constructor,
        /// </summary>
        /// <param name="logger">the loger that take cares the messages</param>
        /// <param name="controller">The Image Processing Controller</param>
        /// <param name="directories"> the paths of the directories we want to listen .</param>
        public ImageServer(ILoggingService logger, IImageController controller, String[] directories, ComunicationServer server, ClientsManager manager)
        {
            m_logging = logger; // setting the logger
            m_controller = controller; // setting the controller
            m_commServer = server;
            DirectoyHandler directoyHandler; 
            // for each directory path we create directory handler.
            for( int i = 0; i < directories.Length; i++)
            {
                directoyHandler = new DirectoyHandler(m_controller, m_logging, directories[i]);
                // register the directory handler to the CommandRecived event .sending commands 
                //through events to directory handler.
                CommandRecieved += directoyHandler.OnCommandRecieved;
                //register the server to the Directory close event .
                directoyHandler.DirectoryClose += OnDirectoryClose;
                // start to listen to the directory
                directoyHandler.StartHandleDirectory(directories[i]);
                m_logging.Log("Created directory handler " + directories[i], Logging.Modal.MessageTypeEnum.INFO);

                // Add the directroty the list of directories that the client mannager has.
                manager.addDirectoryHandler(directoyHandler);
            }
            m_logging.Log("Created server", Logging.Modal.MessageTypeEnum.INFO);
        }
        /// <summary>
        /// OnDirectoryClose.  The Event that will be activated upon Directory handler Closed.
        /// </summary>
        /// <param name="sender"> sender of the event</param>
        /// <param name="e">arguments of the event</param>
        public void OnDirectoryClose (object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                //deleting directory handler action from the event of command recived .
                CommandRecieved -= ((DirectoyHandler)sender).OnCommandRecieved;
                m_logging.Log(e.Message, Logging.Modal.MessageTypeEnum.INFO);
            } catch
            {
                m_logging.Log("failed to close directory handler", Logging.Modal.MessageTypeEnum.FAIL);

            }
        }
        /// <summary>
        /// OnClosedService.
        ///  The Event that will be activated upon the service closed. Invoke command recived event of
        ///  closing the directory handlers.
        /// </summary>
        public void OnClosedService()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,null, "*");
            CommandRecieved?.Invoke(this, commandRecievedEventArgs);
            m_logging.Log("Server notify to close directory handler", Logging.Modal.MessageTypeEnum.INFO);
        }
    }
}
