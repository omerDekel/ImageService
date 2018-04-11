using System.Diagnostics;

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
        private int eventId = 1;

        public ImageService()
        {
            InitializeComponent();

            // Evant log staff
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";

            // Timer stauff
            this.timer = new System.Timers.Timer();
            this.timer.Interval = 60000; // 60 seconds  
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            

        }

        public ImageService(string[] args)
        {
            InitializeComponent();
            string eventSourceName = "MySource";
            string logName = "MyNewLog";
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
    }
}
