using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.ViewModels
{
    interface ISettingsViewModel: INotifyPropertyChanged 
    {
        ObservableCollection<string> DirectoryHandlers { get; }
        string ViewMOutputDirectory { get; }
        string SourceName { get; }
        string LogName { get; }
        string ThumbnailSize { get; }
    }
}
