using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PlayPlan.ViewModels
{
    internal class LogonWebBrowserBinding
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(LogonWebBrowserBinding), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }

        //-------------------------------------------------------------------------------------------------------

        public static readonly DependencyProperty ShouldHandleNavigatedProperty =
        DependencyProperty.RegisterAttached(
            "ShouldHandleNavigated",
            typeof(Boolean),
            typeof(LogonWebBrowserBinding),
            new UIPropertyMetadata(false, ShouldHandleNavigatedPropertyChanged));

        public static Boolean GetShouldHandleNavigated(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(BindableSourceProperty);
        }

        public static void SetShouldHandleNavigated(DependencyObject obj, Boolean value)
        {
            obj.SetValue(ShouldHandleNavigatedProperty, value);
        }

        public static void ShouldHandleNavigatedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                if ((bool)e.NewValue)
                {
                    browser.Navigated += new NavigatedEventHandler(Browser_Navigated);
                }
                else
                {
                    browser.Navigated -= new NavigatedEventHandler(Browser_Navigated);
                }
            }
        }

        private static void Browser_Navigated(object sender, NavigationEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            if (browser != null && browser.Source != null)
            {
                browser.SetValue(BindableSourceProperty, browser.Source.AbsoluteUri);
            }
        }
    }
}
