using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        private string _successCaption = "Success";
        private string _errorCaption = "Failed";

        public void ShowErrorMessageBox(string message)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;
            result = MessageBox.Show(message, _errorCaption, button, icon);
        }

        public void ShowSuccessMessageBox(string message)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(message, _successCaption, button, icon);
        }
    }
}
