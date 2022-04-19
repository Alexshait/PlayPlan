using SHDocVw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlayPlan.Views
{
    /// <summary>
    /// Логика взаимодействия для LogonViewCtrl.xaml
    /// </summary>
    public partial class LogonViewCtrl : UserControl
    {
        public LogonViewCtrl()
        {
            InitializeComponent();
        }

        private void LogonWebBrowserCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {

                FieldInfo webBrowserInfo = LogonWebBrowserCtrl.GetType().GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);

                object comWebBrowser = null;
                object zoomPercent = 130;
                if (webBrowserInfo != null)
                    comWebBrowser = webBrowserInfo.GetValue(LogonWebBrowserCtrl);
                if (comWebBrowser != null)
                {
                    InternetExplorer ie = (InternetExplorer)comWebBrowser;
                    ie.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref zoomPercent, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
