using Gui.Comunication;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using System.Collections.ObjectModel;
namespace Gui.Model
{
    /// <summary>
    /// the model class of the logs.
    /// </summary>
    class LogsModel
    {
        // cliet to connect with image service
        public IClient GuiClient { get; set; }
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
            GuiClient = Client.Instance;
            // register to CommandRecived event of the client 
            GuiClient.CommandRecieved += OnCommandRecieved;
            logsCollection = new ObservableCollection<MessageRecievedEventArgs>();
        }


        public ObservableCollection<MessageRecievedEventArgs> LogsCollection
        {
            get { return logsCollection; }
            set { }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            // if it's get log command
            if (e.CommandID == (int)CommandEnum.LogCommand)
            {
                // translate the Args array of CommandRecievedEventArgs to logs and add them to logCollection
                for (int i = 0; i < e.Args.Length; i += 2)
                {

                    MessageRecievedEventArgs log = new MessageRecievedEventArgs();
                    log.Status = this.FromString(e.Args[i]);
                    log.Message = e.Args[i + 1];
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.LogsCollection.Add(log);
                    });
                }
            }
        }
        /// <summary>
        /// Convert string of the message to MessageType Enum. 
        /// </summary>
        /// <param name="type">string of the message</param>
        /// <returns>converted string to MessageT</returns>
        private MessageTypeEnum FromString(string type)
        {
            if (type.Equals("INFO"))
            {
                return MessageTypeEnum.INFO;
            }
            else if (type.Equals("FAIL"))
            {
                return MessageTypeEnum.FAIL;
            }
            else
            {
                return MessageTypeEnum.WARNING;
            }
        }
    }
}
