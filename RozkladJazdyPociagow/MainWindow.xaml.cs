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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Windows.Navigation;
//using System.Linq;


namespace RozkladJazdyPociagow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            trainConnections = new MyList<TrainConnection>();
            LoadConneFromFile();
            SortMyCollection(); 
         
        }


        MyList<TrainConnection> trainConnections;
        public ConnectionsWindow cw;                   // DRUGIE OKNO
        public AddConWin acw;



        


        private void SortMyCollection()   //sortuje liste polaczen wg daty
        {

            int n = trainConnections.Count;
            do
            {
                for (int i = 0; i < n - 1; i++)
                {
                    if (trainConnections[i].Start_date > trainConnections[i + 1].Start_date)
                    {
                        TrainConnection a = trainConnections[i];
                        trainConnections[i] = trainConnections[i + 1];
                        trainConnections[i + 1] = a;

                        
                    }
                }
                n--;
            }
            while (n > 1);

        }


        void LoadConneFromFile()
        {
            try
            {
               using (StreamReader sr = new StreamReader("connections.txt"))
                {
                    string first;
                    string second;
                    string checkmins;
                    int y, mo, d, h, min;
                    int t_h, t_min;
                    

                    do
                    {
                        first = sr.ReadLine();
                        second = sr.ReadLine();
                        int.TryParse(sr.ReadLine(), out y);
                        int.TryParse(sr.ReadLine(), out mo);
                        int.TryParse(sr.ReadLine(), out d);
                        int.TryParse(sr.ReadLine(), out h); 
                        int.TryParse(sr.ReadLine() , out min);
                        int.TryParse(sr.ReadLine(), out t_h);
                        checkmins = sr.ReadLine();            // w postaci string aby sprawdzic czy nie pobrano null
                        int.TryParse(checkmins, out t_min);

                        DateTime time = new DateTime(1,1,1,t_h, t_min, 0);

                        

                        if (checkmins != null)
                           trainConnections.Add(new TrainConnection(first, second, y, mo, d, h, min, time));
                         
                         
                           

                    } while (checkmins != null);


                   // MessageBox.Show(trainConnections.Count.ToString() + " Objects loaded.");
                }
            }
            catch
            {
                MessageBox.Show("File could not be load! Probably do not exist.");
            }

        }
         


        private void HideAllGrids()
        {
            //gridLookingTrains.Visibility = Visibility.Hidden;
            gridTickets.Visibility       = Visibility.Hidden;
            gridContact.Visibility       = Visibility.Hidden;
            gridNews.Visibility          = Visibility.Hidden;
        }


        private void BackgroundClick(object sender, MouseButtonEventArgs e)
        {
            HideAllGrids();

            CalenderWin1.Visibility = Visibility.Hidden;
        }



       

        private void LookingTrains(object sender, RoutedEventArgs e)   //Po kliknieciu przycisku szukaj (w drugim oknie)
        {
            cw = new ConnectionsWindow();
            cw.Show(); 
            cw.Owner = this;
            
            HideAllGrids();

            

          // cw.dataGridConnections.Items.Add(trainConnections[0]);
        }

        private void ShowTicketsPageClick(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            gridTickets.Visibility = Visibility.Visible;
        }

       
        private void ContactPageClick(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            gridContact.Visibility = Visibility.Visible;
        }

        private void ShowNewsPageClick(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            gridNews.Visibility = Visibility.Visible;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void findConnectionClick(object sender, RoutedEventArgs e)
        {
             
            string fromTown = cw.goFromBoxWin2.Text; 
            string toTown = cw.goToBoxWin2.Text;
            int n = trainConnections.Count;   // ilosc wszystkich polaczen w bazie
            DateTime leave_date;
             
            convertToBigChar(ref fromTown);
            convertToBigChar(ref toTown);

            if (cw.DatePickerWin2.SelectedDate == null)
            {
                leave_date = DateTime.Today;
                cw.DatePickerWin2.SelectedDate = DateTime.Today;
            }
            else
                leave_date = (DateTime)cw.DatePickerWin2.SelectedDate;  //pobranie daty z datepickera
             
              

            if (fromTown != "" || toTown != "")
            {
                

                if (fromTown != "" && toTown != "")
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (fromTown == trainConnections[i].Start_town && toTown == trainConnections[i].End_town  &&  trainConnections[i].Start_date >= leave_date) cw.dataGridConnections.Items.Add(trainConnections[i]);
                    } 
                }

                else if (fromTown != "")
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (fromTown == trainConnections[i].Start_town  &&  trainConnections[i].Start_date >= leave_date) cw.dataGridConnections.Items.Add(trainConnections[i]);
                    }
                }
                else if (toTown != "")
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (toTown == trainConnections[i].End_town    &&   trainConnections[i].Start_date >= leave_date) cw.dataGridConnections.Items.Add(trainConnections[i]);
                    }
                }




                if (cw.dataGridConnections.Items.Count == 0) MessageBox.Show("Nie znaleniono połączeń.", "Brak połączeń");
                else
                {
                    ShowConnections();
                    cw.Width += 116;
                    cw.Left -= 58;
                    cw.buttonFindNew.Visibility = Visibility.Visible;
                }

            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (trainConnections[i].Start_date >= leave_date) cw.dataGridConnections.Items.Add(trainConnections[i]);
                }
                  
                if (cw.dataGridConnections.Items.Count == 0) MessageBox.Show("Nie znaleniono połączeń.", "Brak połączeń");
                else
                {
                    ShowConnections();
                    cw.Width += 116;
                    cw.Left -= 58;
                    cw.buttonFindNew.Visibility = Visibility.Visible;  

                }

            }


           

        }


        void ShowConnections()
        {
            cw.buttonFindConnection.Visibility = Visibility.Hidden;
            cw.label1Win2.Visibility = Visibility.Hidden;
            cw.label2Win2.Visibility = Visibility.Hidden;
            cw.label3Win2.Visibility = Visibility.Hidden;
            cw.goFromBoxWin2.Visibility = Visibility.Hidden;
            cw.goToBoxWin2.Visibility = Visibility.Hidden;

            cw.dataGridConnections.Visibility = Visibility.Visible; 
        }

        public void HideConnections()
        {
            cw.buttonFindConnection.Visibility = Visibility.Visible;
            cw.label1Win2.Visibility = Visibility.Visible;
            cw.label2Win2.Visibility = Visibility.Visible;
            cw.label3Win2.Visibility = Visibility.Visible;
            cw.goFromBoxWin2.Visibility = Visibility.Visible;
            cw.goToBoxWin2.Visibility = Visibility.Visible;

            cw.dataGridConnections.Visibility = Visibility.Hidden;
        }


        public interface IMainWindow
        {
            void RefreshCustomers();
            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ShowCalender(object sender, RoutedEventArgs e)
        {
            CalenderWin1.Visibility = Visibility.Visible;
        }



        private void convertToBigChar(ref string word)
        {
            string help = "";

            if (word != ""     &&    word[0] >= 97)
            {
                help += (char)(word[0] - 32);

                for(int i=1;   i<word.Length;   i++)
                {
                    help += word[i];
                }

                word = help;
            }

        }



        private void addConnectionClick(object sender, RoutedEventArgs e)
        {
            acw = new AddConWin();
            acw.Show();
        }



        public void addConnectionToList(TrainConnection t)
        {
            trainConnections.Add(t); 

            //save to file 
            try
            {

                using (StreamWriter sw = new StreamWriter("connections.txt", true))
                {
                     
                    sw.WriteLine(t.Start_town);
                    sw.WriteLine(t.End_town);
                    sw.WriteLine(t.Start_date.Year);
                    sw.WriteLine(t.Start_date.Month);
                    sw.WriteLine(t.Start_date.Day);
                    sw.WriteLine(t.Start_date.Hour);
                    sw.WriteLine(t.Start_date.Minute);
                    sw.WriteLine(t.Connect_time.Hour);
                    sw.WriteLine(t.Connect_time.Minute);
                      
                }

            }
            catch
            {
                MessageBox.Show("Couldn't save Connection to file.");
            }

            SortMyCollection();  //Sortowanie listy po zapisie do pliku ostatniego elementu

        }






    }

}
