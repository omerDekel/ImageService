﻿using Gui.Model;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.ViewModels
{
    class LogsViewModel 
    {
        private LogsModel model;
        /// <summary>
        /// constructor .
        /// </summary>
        public LogsViewModel()
        {
            model = new LogsModel();
        }
        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            get
            {
                return model.LogsCollection;
            }
        }

        public LogsModel Model { get; set; }
    }
}
