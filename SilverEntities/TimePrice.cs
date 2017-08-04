using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverEntities
{
    public class TimePrice
    {
        public int ID { get; set; }
        public int ID_Escort { get; set; }
        public int Time_In_Hour { get; set; }
        public int Price { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
