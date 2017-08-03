using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class EscortBLL
    {
        private EscortDAL escortDAL;

        public EscortDAL EscortDAL
        {
            get
            {
                if (escortDAL == null)
                    escortDAL = new EscortDAL();
                return escortDAL;
            }
        }

        public int InsertEscort(Escort escort)
        {
            return EscortDAL.InsertEscort(escort);
        }

        public bool UpdateEscort(Escort escort)
        {
            return EscortDAL.UpdateEscort(escort);
        }

        public Escort getEscortByID(int id)
        {
            return EscortDAL.getEscortByID(id);
        }

        public bool DeleteEscortByID(int id)
        {
            return EscortDAL.DeleteEscortByID(id);
        }

        public List<Escort> ListEscortsByPartialName(string partialName)
        {
            return EscortDAL.ListEscortsByPartialName(partialName);
        }

        public List<Escort> ListEscorts()
        {
            return EscortDAL.ListEscorts();
        }
    }
}
