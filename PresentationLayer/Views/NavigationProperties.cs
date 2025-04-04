using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Views
{
    public static class NavigationProperties
    {
        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.RegisterAttached(
                "NavigationUri",
                typeof(string),
                typeof(NavigationProperties),
                new PropertyMetadata(null));

        public static void SetNavigationUri(DependencyObject element, string value)
        {
            element.SetValue(NavigationUriProperty, value);
        }

        public static string GetNavigationUri(DependencyObject element)
        {
            return (string)element.GetValue(NavigationUriProperty);
        }
    }
}
