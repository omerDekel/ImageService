using Gui.Model;
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
    class LogsViewModel : INotifyPropertyChanged
    {
        private LogsModel model;
        //constructor
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
