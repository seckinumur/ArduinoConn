using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConn.AMDB.DBCLASS
{
  public  class MainLog1
    {
        public int ID { get; set; }
        public double Temperature { get; set; }
        public double Damp { get; set; }
        public string Light { get; set; }
        public DateTime Time { get; set; }
        public DateTime Month { get; set; }
        public DateTime Days { get; set; }
        public DateTime Years { get; set; }
    }
}
