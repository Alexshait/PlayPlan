using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlayPlan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow() { DataContext = new ViewModels.MainWindowViewModel() };
            window.Show();
            //var logonViewModel = new ViewModels.LogonViewModel
            //{
            //    WebAddress = @"https://oauth.vk.com/authorize?client_id=8073115&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.131"
            //};

            ////DataContext = model;

            //var logonView = new Views.LogonView();
            //logonView.DataContext = logonViewModel;
            //logonView.Show();
            //var txt = logon.LogonWebBrowser.Source.AbsoluteUri;
        }
    }
}
