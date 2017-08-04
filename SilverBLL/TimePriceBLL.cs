using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class TimePriceBLL
    {
        private TimePriceDAL timePriceDAL;

        public TimePriceDAL TimePriceDAL
        {
            get
            {
                if (timePriceDAL == null)
                    timePriceDAL = new TimePriceDAL();
                return timePriceDAL;
            }
        }

        public int InsertTimePrice(TimePrice timePrice)
        {
            return TimePriceDAL.InsertTimePrice(timePrice);
        }

        public bool UpdateTimePrice(TimePrice timePrice)
        {
            return TimePriceDAL.UpdateTimePrice(timePrice);
        }

        public TimePrice getTimePriceByID(int id)
        {
            return TimePriceDAL.getTimePriceByID(id);
        }

        public bool DeleteTimePriceByID(int id)
        {
            return TimePriceDAL.DeleteTimePriceByID(id);
        }

        public List<TimePrice> ListTimePriceByEscortID(int escortID)
        {
            return TimePriceDAL.ListTimePriceByEscortID(escortID);
        }

        public List<TimePrice> ListTimePrices()
        {
            return TimePriceDAL.ListTimePrices();
        }
    }
}
