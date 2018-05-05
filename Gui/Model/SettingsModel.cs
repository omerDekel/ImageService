using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Model
{
    class SettingsModel : ISettingsModel
    {
        public SettingsModel() { }
        public ObservableCollection<string> DirectoryHandlers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OutputDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SourceName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LogName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ThumbnailSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
