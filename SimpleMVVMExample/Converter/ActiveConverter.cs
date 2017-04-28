using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleMVVMExample.Converter
{
    // Converts char from OracleDB to Boolean
    class ActiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char activeChar = (char)value;

            switch (activeChar)
            {
                case '1':
                    return true;
                case '0':
                    return false;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool activeBool = (bool) value;

            switch (activeBool)
            {
                case true:
                    return '1';
                case false:
                    return '0';
                default:
                    return '0';
            }
        }
    }
}
