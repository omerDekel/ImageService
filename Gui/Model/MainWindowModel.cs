using Gui.Comunication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Model
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private bool connectedToServer;
        public bool ConnectedToServer
        {
            get
            {
                return connectedToServer;
            }
            set
            {
                connectedToServer = value;
                NotifyPropertyChanged("ConnectedToServer");
            }
        }
        public IClient client { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindowModel()
        {
            client = Client.Instance;
            connectedToServer = !client.Stop;
        }
        protected void NotifyPropertyChanged(string name)
        {

            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(name));
        }


    }
}
