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
    public class TimePricesController : ApiController
    {
        private TimePriceBLL timePriceBLL;

        public TimePriceBLL TimePriceBLL
        {
            get
            {
                if (timePriceBLL == null)
                    timePriceBLL = new TimePriceBLL();
                return timePriceBLL;
            }
        }

        // GET api/values
        public JsonResult<List<TimePrice>> Get()
        {
            List<TimePrice> timePrices = TimePriceBLL.ListTimePrices();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(timePrices, serializerSettings);
        }

        // GET api/values/5
        public JsonResult<TimePrice> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(TimePriceBLL.getTimePriceByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string timePrice)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            TimePrice u = JsonConvert.DeserializeObject<TimePrice>(timePrice, serializerSettings);
            return TimePriceBLL.InsertTimePrice(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string timePrice)
        {
            TimePrice u = JsonConvert.DeserializeObject<TimePrice>(timePrice);
            u.ID = id;
            return new { success = TimePriceBLL.UpdateTimePrice(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = TimePriceBLL.DeleteTimePriceByID(id) };
        }
    }
}
