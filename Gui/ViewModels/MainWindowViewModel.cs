using Gui.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private MainWindowModel model;

        public MainWindowViewModel()
        {
            model = new MainWindowModel();
            model.PropertyChanged += OnPropertyChanged;
         }
        public bool ConnectedToServer
        {
            get
            {
                return model.ConnectedToServer;
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /**/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

    }
}
