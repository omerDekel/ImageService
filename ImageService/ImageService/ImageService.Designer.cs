using System.Diagnostics;
using System.Configuration;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Controller;
using ImageService.Server;



namespace ImageService
{
    partial class ImageService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Diagnostics.EventLog eventLog1;
        private System.Timers.Timer timer;
        //private
        private int eventId = 1;


        public ImageService()
        {
            InitializeComponent();
            
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

            // Define the ImageModel
            int size = int.Parse(this.getAppConfigValue(thumbnailSizeKey));
            this.modal = new ImageServiceModal(this.getAppConfigValue(outPutDirKey), size);

            // Define the controler
            this.controller = new ImageController(this.modal);

            // Define the server
            string directories = this.getAppConfigValue(sourceDirectoriesKey);
            string[] directoryArray = directories.Split(';');
            this.m_imageServer = new ImageServer(this.logging, this.controller, directoryArray); 
        }

        public ImageService(string[] args)
        {
            InitializeComponent();
            string logSourceNameKey = "SourceName";
            string logNameKey = "LogName";

            string eventSourceName = this.getAppConfigValue(logSourceNameKey);
            string logName = this.getAppConfigValue(logNameKey);
            if (args.Length > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Length > 1)
            {
                logName = args[1];
            }
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // eventLog1
            // 
            this.eventLog1.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLog1_EntryWritten);
            // 
            // ImageService
            // 
            this.ServiceName = "ImageService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }


        #endregion

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        private string getAppConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
