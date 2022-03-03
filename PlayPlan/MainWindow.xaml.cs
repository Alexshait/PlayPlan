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
            using (var db = new PlayPlanContext())
            {
                Person person = new Person()
                {
                    //ID = 1,
                    PersonName = "Пирогов Сергей"
                };
                //db.Persons.Add(person);
                //db.SaveChanges();
            }

            var model = new ViewModels.MainWindowViewModel
            {
                WebAddress = @"https://oauth.vk.com/authorize?client_id=8073115&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.131"
            };

            DataContext = model;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.LogonWebBrowser.Source.AbsoluteUri);
        }

        private void LogonWebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            MessageBox.Show(this.LogonWebBrowser.Source.AbsoluteUri);
        }
    }
}
