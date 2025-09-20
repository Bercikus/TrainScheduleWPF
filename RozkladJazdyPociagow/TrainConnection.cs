using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozkladJazdyPociagow
{
    public class TrainConnection
    {
        string start_town;
        string end_town;
        DateTime start_date;
        DateTime connection_time;

        public TrainConnection(string start, string end, int year, int month, int day, int hour, int minute, DateTime con_time)
        {
            start_town = start;
            end_town = end;
            start_date = new DateTime(year, month, day,hour,minute, 0);
            connection_time = con_time;
        }


        public string Start_town
        {
            get { return start_town; }
        }

        public string End_town
        {
            get { return end_town; }
        }

        public DateTime Start_date
        {
            get { return start_date; }
        }

        public DateTime Connect_time
        {
            get { return connection_time; }
        }


        public string Connection_time
        {
            get { return connection_time.TimeOfDay.ToString(); }
        }

        public string Get_date   //Zwraca tylkodate bez godzin
        {
            get { return start_date.Day.ToString() + " / " + start_date.Month.ToString() + " / " + start_date.Year; }
        }

        public string Get_time
        {
            get  {  return start_date.TimeOfDay.ToString(); }
        }

 


    }

}
