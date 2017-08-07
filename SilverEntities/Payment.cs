using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverEntities
{
    public class Payment
    {
        public int ID { get; set; }
        public int ID_Pay_Method { get; set; }
        public int ID_Meeting { get; set; }
        public double Value { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
