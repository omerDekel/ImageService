using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Gui.ViewModels
{
    class TypeToBackgroundConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("Must convert to a brush!");
            MessageRecievedEventArgs messageRecived = (MessageRecievedEventArgs)value;
            MessageTypeEnum messageType = messageRecived.Status;
            switch(messageType)
            {
                case MessageTypeEnum.FAIL:
                    return Brushes.Red;
                case MessageTypeEnum.INFO:
                    return Brushes.Green;
                case MessageTypeEnum.WARNING:
                    return Brushes.Yellow;

            }
            return Brushes.Transparent;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
