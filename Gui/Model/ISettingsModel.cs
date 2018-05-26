using Gui.Comunication;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Model
{
    /// <summary>
    /// interfacce for the model of the settings .
    /// </summary>
    interface ISettingsModel: INotifyPropertyChanged
    {
        ObservableCollection<string> DirectoryHandlers { get; set; }
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string ThumbnailSize { get; set; }
        // the client we connect thougth it with image Service  
        IClient ClientGui { get; set; }
        // sending command to server through the client .
        void CommandToServer(CommandRecievedEventArgs e);
        // remove directory from DirectoryHandlers collection.
        void RemoveDirectory(string dir);

    }
}
