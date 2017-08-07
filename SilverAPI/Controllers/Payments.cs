using Newtonsoft.Json;
using SilverBLL;
using SilverEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SilverAPI.Controllers
{
    public class PaymentsController : ApiController
    {
        private PaymentBLL paymentBLL;
         
        public PaymentBLL PaymentBLL
        {
            get
            {
                if (paymentBLL == null)
                    paymentBLL = new PaymentBLL();
                return paymentBLL;
            }
        }

        // GET api/values
        public JsonResult<List<Payment>> Get()
        {
            List<Payment> payments = PaymentBLL.ListPayments();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(payments, serializerSettings);
        }

        // GET api/values/5
        public JsonResult<Payment> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(PaymentBLL.getPaymentByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string payment)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            Payment u = JsonConvert.DeserializeObject<Payment>(payment, serializerSettings);
            return PaymentBLL.InsertPayment(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string payment)
        {
            Payment u = JsonConvert.DeserializeObject<Payment>(payment);
            u.ID = id;
            return new { success = PaymentBLL.UpdatePayment(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = PaymentBLL.DeletePaymentByID(id) };
        }
    }
}
