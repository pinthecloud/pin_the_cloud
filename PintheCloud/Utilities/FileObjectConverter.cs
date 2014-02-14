﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PintheCloud.Utilities
{
    public class FileObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string isFolder = (string)value;

            if ("folder".Equals(isFolder))
                return new Uri("/Assets/Icons/folder.png", UriKind.Relative);
            else
                return new Uri("/Assets/Icons/file.png", UriKind.Relative);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
