using PlayPlan.DataModel;
using PlayPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }


        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).DoubleClickCmd.Execute(CommentsLst.SelectedItem);
        }
    }
}
