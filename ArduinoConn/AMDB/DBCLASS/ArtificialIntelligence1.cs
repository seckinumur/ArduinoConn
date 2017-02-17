using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConn.AMDB.DBCLASS
{
   public class ArtificialIntelligence1
    {
        public int ID { get; set; }
        public double AverageTemperature { get; set; }
        public double Damp { get; set; }
        public string Lights { get; set; }
        public DateTime NightStart { get; set; }
        public DateTime DayStart { get; set; }
        public DateTime Year { get; set; }
        public DateTime Month { get; set; }
        public DateTime Week { get; set; }
        public DateTime Day { get; set; }
    }
}
