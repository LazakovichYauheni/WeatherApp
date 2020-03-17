using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Weather.UWP.Converters
{
    public class JsonDataIconToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return new BitmapImage();
            }

            var img = parameter == null ?
                new BitmapImage(new Uri(value.ToString())) :
                new BitmapImage(new Uri(string.Format((string)parameter, value)));

            img.ImageFailed += (s, e) => img.UriSource = new Uri(string.Format((string)parameter, ""));

            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
