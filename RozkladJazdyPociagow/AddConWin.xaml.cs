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
    /// Logika interakcji dla klasy AddConWin.xaml
    /// </summary>
    public partial class AddConWin : Window
    {
        public AddConWin()
        {
            InitializeComponent();
            this.Left = Application.Current.MainWindow.Left + 130;
            this.Top = Application.Current.MainWindow.Top + 50;
        }


        string password = "student";



        private void onKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                  
                if (passBoxWin3.Password == password)
                {
                    gridInputPass.Visibility      = Visibility.Hidden;
                    gridAddConnections.Visibility = Visibility.Visible;

                }
                else
                {
                    passBoxWin3.Password = null;
                    label3Win3.Visibility = Visibility.Visible;
                }

            }
             
        }



         


        private void addConnectionClick(object sender, RoutedEventArgs e)
        {

            string from = textBoxStart.Text;
            string to = textBoxEnd.Text; 
            string hour = textBoxHour.Text;
            string min = textBoxMin.Text;
            string timeHr = textBoxTimeHr.Text;
            string timeMin = textBoxTimeMin.Text;
            int y=0, m=0, d=0;


            if (from == "" || to == "" || hour == "" || min == "" || timeHr == ""
                                          || timeMin == "" || this.datePickerWin3.SelectedDate == null)
            {

                MessageBox.Show("Nie wszystkie pola zostały wypełnione!");
                 
            } 
            else   //sprawdzanie poprawnosci daty i godziny
            {
                 
                DateTime date = (DateTime)this.datePickerWin3.SelectedDate;
                y = date.Year;
                m = date.Month;
                d = date.Day;


                try
                {

                    if (  int.Parse(hour) >= 24 || int.Parse(min) >= 60 || int.Parse(timeHr) >= 24 || int.Parse(timeMin) >= 60  ) MessageBox.Show("Nie prawidłowa godzina lub minuta!");

                    else
                    {

                        TrainConnection t = new TrainConnection (    from, to, y, m, d,  
                                                                     int.Parse(hour),          
                                                                     int.Parse(min),   
                                                                     new DateTime(    1, 1, 1, int.Parse(timeHr), int.Parse(timeMin),  0   )       
                                                                );


                        (Application.Current.MainWindow as MainWindow).addConnectionToList(t);  //MainWindow  method

                        MessageBox.Show("Dodano połączenie.", "Udało sie");
                        this.Close();

                    }

                }
                catch
                {
                    MessageBox.Show("Nie prawidłowa godzina lub minuta!");
                }


            }


        }





    }
}
