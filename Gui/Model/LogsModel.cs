using Gui.Comunication;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using ImageService.Logging.Modal;
using System.Collections.ObjectModel;
namespace Gui.Model
{
    /// <summary>
    /// 
    /// </summary>
    class LogsModel//:INotifyPropertyChanged
    {
        public IClient GuiClient { get; set; }
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
            GuiClient = Client.Instance;
            GuiClient.CommandRecieved += OnCommandRecieved;
            logsCollection = new ObservableCollection<MessageRecievedEventArgs>();
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.WARNING, Message = "hiiiii" };
            MessageRecievedEventArgs messaeRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.INFO, Message = "hiiiii" };
            LogsCollection.Add(messageRecievedEventArgs);
            LogsCollection.Add(messaeRecievedEventArgs);

        }

            
        public ObservableCollection<MessageRecievedEventArgs> LogsCollection
        {
            get { return logsCollection; }
            set { }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {

            if(e.CommandID == (int)CommandEnum.LogCommand)
            {
               for( int i = 0; i < e.Args.Length; i+=2)
                {
                        
                    MessageRecievedEventArgs log = new MessageRecievedEventArgs();
                    log.Status = this.fromString(e.Args[i]);
                    log.Message = e.Args[i + 1];
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.LogsCollection.Add(log);
                    });
                }
            }
        }

        private MessageTypeEnum fromString(string type)
        {
            if (type.Equals("INFO"))
            {
                return MessageTypeEnum.INFO;
            } else if(type.Equals("FAIL"))
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
