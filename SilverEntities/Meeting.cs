using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverEntities
{
    public class Meeting
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Escort { get; set; }
        public int Total_Time_In_Hours { get; set; }
        public double Total_Price { get; set; }
        public int ID_Credit_Card { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
