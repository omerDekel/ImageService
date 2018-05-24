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
    class LogsModel
    {
        public IClient GuiClient { get; set; }
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            /*if(e.CommandID == (int)CommandEnum.LogCommand)
            {

               for( int i = 0; i < e.Args.Length; i++)
                {
                    MessageRecievedEventArgs log = (MessageRecievedEventArgs)e.
                    logsCollection.Add((MessageRecievedEventArgs)(e.Args[i]));
                }*/
            }
        }
    }
}
