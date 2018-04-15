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

        /// <summary>
        /// An old constractor. Initialize the service's components.
        /// </summary>
        public ImageService()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// The old service constractor, only initialize the logging.
        /// </summary>
        /// <param name="args"></param>
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
            // ImageService
            // 
            this.ServiceName = "ImageService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }


        #endregion

        /// <summary>
        /// The monitoring part of the service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        /// <summary>
        /// Return the appconfig value of the key the function recives.
        /// </summary>
        /// <param name="key">The key to be ask.</param>
        /// <returns></returns>
        private string getAppConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
