using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        //private const string DBaseName = "Playplan.db";
        private readonly string DBaseName = ConfigurationManager.AppSettings.Get("DBaseName") ?? "Playplan.db";
        private readonly string Version = typeof(App).Assembly.GetName().Version.ToString();
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            string DbPath = SetDbPath();
            var window = new MainWindow() { DataContext = new ViewModels.MainWindowViewModel(DbPath) };
            window.Show();

        }

        private string SetDbPath()
        {
            string DefaultPath = Path.Join(Environment.CurrentDirectory, DBaseName);
            string folder = Environment.CurrentDirectory;
            if (!File.Exists(DefaultPath))
            {
                folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (folder != "")
                {
                    if (Directory.Exists(folder + "\\Playplan"))
                    {
                        folder = folder + "\\Playplan\\";
                    }
                    else
                    {
                        Directory.CreateDirectory(folder + "\\Playplan");
                        folder = folder + "\\Playplan\\";
                    }
                }
                else
                {
                    MessageBox.Show($"Отсутствует системный каталог {Environment.SpecialFolder.ApplicationData}", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }
            }

            return Path.Join(folder, DBaseName);
        }
    }
}
