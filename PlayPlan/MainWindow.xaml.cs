using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlayPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(this.LogonWebBrowser.Source.AbsoluteUri);
        }

        private void LogonWebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            //MessageBox.Show(this.LogonWebBrowser.Source.AbsoluteUri);
        }
    }
}
