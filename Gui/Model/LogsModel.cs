<<<<<<< HEAD
﻿using Gui.Comunication;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> parent of ed5b55d... after merge

namespace Gui.Model
{
    class LogsModel: INotifyPropertyChanged
    {
<<<<<<< HEAD
        public IClient GuiClient { get; set; }
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
            GuiClient = Client.Instance;
            GuiClient.CommandRecieved += OnCommandRecieved;
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.LogCommand,null,"");
            logsCollection = new ObservableCollection<MessageRecievedEventArgs>();
            GuiClient.CommandToServer(command);
            /*MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.WARNING, Message = "hiiiii" };
            MessageRecievedEventArgs messaeRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.INFO, Message = "hiiiii" };

            //messageRecievedEventArgs.Message = "hiiiii";
            //messageRecievedEventArgs.Status = 0;
            LogsCollection.Add(messageRecievedEventArgs);
            LogsCollection.Add(messaeRecievedEventArgs);*/

        }

            
        public ObservableCollection<MessageRecievedEventArgs> LogsCollection
        {
            get { return logsCollection; }
            set { }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
        }
=======
>>>>>>> parent of ed5b55d... after merge
    }
}
