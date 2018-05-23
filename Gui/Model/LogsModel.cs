<<<<<<< HEAD
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

=======
﻿using ImageService.Logging.Modal;
using System.Collections.ObjectModel;
>>>>>>> parent of 10b194c... Settings model  get commands from the server
namespace Gui.Model
{
    class LogsModel
    {
<<<<<<< HEAD
<<<<<<< HEAD
        public IClient GuiClient { get; set; }
=======
>>>>>>> parent of 10b194c... Settings model  get commands from the server
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
            logsCollection = new ObservableCollection<MessageRecievedEventArgs>();
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.WARNING, Message = "hiiiii" };
            MessageRecievedEventArgs messaeRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.INFO, Message = "hiiiii" };

            //messageRecievedEventArgs.Message = "hiiiii";
            //messageRecievedEventArgs.Status = 0;
            LogsCollection.Add(messageRecievedEventArgs);
            LogsCollection.Add(messaeRecievedEventArgs);

        }

            
        public ObservableCollection<MessageRecievedEventArgs> LogsCollection
        {
            get { return logsCollection; }
            set { }
        }
<<<<<<< HEAD

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
        }
=======
>>>>>>> parent of ed5b55d... after merge
=======
>>>>>>> parent of 10b194c... Settings model  get commands from the server
    }
}
