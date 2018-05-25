using Gui.Comunication;
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
    /// 
    /// </summary>
    interface ISettingsModel: INotifyPropertyChanged
    {
        ObservableCollection<string> DirectoryHandlers { get; set; }
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string ThumbnailSize { get; set; }
        IClient ClientGui { get; set; }
    }
}
