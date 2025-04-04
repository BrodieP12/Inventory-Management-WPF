using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Helpers
{
    public class ConfirmationDialog
    {
        public bool ShowConfirmationDialog(string message, string title)
        {
            MessageBoxResult result = (MessageBoxResult)ModernWpf.MessageBox.Show(
                message,
                title,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            return (result == MessageBoxResult.Yes);
        }
    }
}
