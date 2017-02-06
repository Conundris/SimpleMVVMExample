using System;
using System.Windows;

namespace SimpleMVVMExample.Exceptions
{
    class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message) : base(message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
