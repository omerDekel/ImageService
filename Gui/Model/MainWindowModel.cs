using Gui.Comunication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Model
{
    /// <summary>
    /// model of the main window. 
    ///  background is gray if there's no connection
    /// between client and server .
    /// </summary>
    class MainWindowModel : INotifyPropertyChanged
    {
        // true if there's connection, else false .
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
        /// <summary>
        /// constructor.
        /// </summary>
        public MainWindowModel()
        {
            // connecting to the server Image service .
            client = Client.Instance;
            // if the connection didn't stop then there's a connection .
            connectedToServer = !client.Stop;
        }
        /// <summary>
        /// NotifyPropertyChanged invoke event of PropertyChangedEventHandler.
        /// </summary>
        /// <param name="name"> name of the changed property</param>
        protected void NotifyPropertyChanged(string name)
        {

            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(name));
        }


    }
}
