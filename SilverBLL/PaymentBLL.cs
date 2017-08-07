using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class PaymentBLL
    {
        private PaymentDAL paymentDAL;

        public PaymentDAL PaymentDAL
        {
            get
            {
                if (paymentDAL == null)
                    paymentDAL = new PaymentDAL();
                return paymentDAL;
            }
        }

        public int InsertPayment(Payment payment)
        {
            return PaymentDAL.InsertPayment(payment);
        }

        public bool UpdatePayment(Payment payment)
        {
            return PaymentDAL.UpdatePayment(payment);
        }

        public Payment getPaymentByID(int id)
        {
            return PaymentDAL.getPaymentByID(id);
        }

        public bool DeletePaymentByID(int id)
        {
            return PaymentDAL.DeletePaymentByID(id);
        }

        public List<Payment> ListPaymentByEscortID(int escortID)
        {
            return PaymentDAL.ListPaymentByEscortID(escortID);
        }

        public List<Payment> ListPayments()
        {
            return PaymentDAL.ListPayments();
        }
    }
}
