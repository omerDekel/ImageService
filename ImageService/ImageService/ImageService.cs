using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;
using ImageService.Logging.Modal;
using System.Configuration;
using ImageService.Infrastructure;

namespace ImageService
{

    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class ImageService : ServiceBase
    {   
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;
        private ComunicationServer comServer;

        /// <summary>
        /// This function is called when the service starts.
        /// The functions initialized the sercice components and starts listning to the folders.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // What the function really does
            string sourceDirectoriesKey = "Handler";
            string outPutDirKey = "OutputDir";
            string logSourceNameKey = "SourceName";
            string logNameKey = "LogName";
            string thumbnailSizeKey = "ThumbnailSize";

            // Define the evant log, messages from this application will be writeen to.
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(this.getAppConfigValue(logSourceNameKey)))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    this.getAppConfigValue(logSourceNameKey), this.getAppConfigValue(logNameKey));
            }
            eventLog1.Source = this.getAppConfigValue(logSourceNameKey);
            eventLog1.Log = this.getAppConfigValue(logNameKey);

            // Defime the service's timer
            this.timer = new System.Timers.Timer();
            this.timer.Interval = 60000; // 60 seconds  
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);

            // Define the logger
            this.logging = new LoggingService();
            // Regidter the the logging model event o when a part of the program
            // send a request to the logging model in order to write a log.
            // the funtion writeLog will be invoked and it will write the massage to the event viewer.
            this.logging.MessageRecieved += this.writeLog;

            // Define the logging buffer helps to send logs to the clients
            LogingBuffer logsBuffer = new LogingBuffer();

            // Define the ImageModel
            int size = int.Parse(this.getAppConfigValue(thumbnailSizeKey));
            this.modal = new ImageServiceModal(this.getAppConfigValue(outPutDirKey), size);

            // Define the controler
            this.controller = new ImageController(this.modal, logsBuffer);

            // Define the ClientsManagget that will manage the coimunication with the diffrent clients.
            ClientsManager managger = new ClientsManager(this.controller, this.logging);

            // Define the cominication server
            this.comServer = new ComunicationServer("127.0.0.1", 8000, managger);

            // Define the server
            string directories = this.getAppConfigValue(sourceDirectoriesKey);
            string[] directoryArray = directories.Split(';');
            this.m_imageServer = new ImageServer(this.logging, this.controller, directoryArray, comServer, managger);

            eventLog1.WriteEntry("In OnStart");
            this.timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        /// <summary>
        /// This functions is called when the service stops.
        /// The function make the service stop listening the the folders.
        /// </summary>
        protected override void OnStop()
        {
            this.m_imageServer.OnClosedService();
            eventLog1.WriteEntry("In onStop.");
        }

        /// <summary>
        /// This function is called when the service is continue working after it sttopped.
        /// </summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        /// <summary>
        /// The function writes a log to the service's event log.
        /// </summary>
        /// <param name="sender">The object that sent the massage</param>
        /// <param name="args">The massage argumnts.</param>
        private void writeLog(object sender, MessageRecievedEventArgs args)
        {
            switch (args.Status)
            {
                case MessageTypeEnum.INFO: {
                        eventLog1.WriteEntry(args.Message, EventLogEntryType.Information);
                        break;
                    }
                case MessageTypeEnum.FAIL:
                {
                        eventLog1.WriteEntry(args.Message, EventLogEntryType.Error);
                        break;
                }
                case MessageTypeEnum.WARNING:
                {
                        eventLog1.WriteEntry(args.Message, EventLogEntryType.Warning);
                        break;
                }
            }
            
        }

    }
}
