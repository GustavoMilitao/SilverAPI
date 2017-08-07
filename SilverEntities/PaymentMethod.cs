using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverEntities
{
    public class PaymentMethod
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Pay_Type { get; set; }
        public int? ID_Credit_Card { get; set; }
        public bool? Active { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
