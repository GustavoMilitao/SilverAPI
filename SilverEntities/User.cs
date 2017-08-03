using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverEntities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Addresscode { get; set; }
        public string Country { get; set; }
        public string DDI { get; set; }
        public string DDD { get; set; }
        public string Phonenumber { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
