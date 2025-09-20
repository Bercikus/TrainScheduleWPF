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

namespace RozkladJazdyPociagow
{
    /// <summary>
    /// Logika interakcji dla klasy ConnectionsWindow.xaml
    /// </summary>
    public partial class ConnectionsWindow : Window
    {
        public ConnectionsWindow()
        {
            InitializeComponent(); 
            this.Left = Application.Current.MainWindow.Left + 130;
            this.Top = Application.Current.MainWindow.Top + 50;
            
            
        }

        private void FindConnectionClick(object sender, RoutedEventArgs e)
        {
            object o = new object();
            RoutedEventArgs r = new RoutedEventArgs();
            (Application.Current.MainWindow as MainWindow).findConnectionClick(o,r);  //uzycie metody z okna 1

             

        }

        private void FindNewClick(object sender, RoutedEventArgs e)  //Height="415" Width="410"
        { 
            (Application.Current.MainWindow as MainWindow).HideConnections();
            buttonFindNew.Visibility = Visibility.Hidden;
            this.Height = 415;
            this.Width = 410; 
            this.Left += 50;
            dataGridConnections.Items.Clear();
           

        }
    }
}
