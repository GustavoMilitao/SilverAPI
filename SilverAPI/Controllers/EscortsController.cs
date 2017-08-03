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
    public class EscortsController : ApiController
    {
        private EscortBLL escortBLL;

        public EscortBLL EscortBLL
        {
            get
            {
                if (escortBLL == null)
                    escortBLL = new EscortBLL();
                return escortBLL;
            }
        }

        // GET api/values
        public JsonResult<List<Escort>> Get()
        {
            List<Escort> escorts = EscortBLL.ListEscorts();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(escorts, serializerSettings);
        }

        public JsonResult<List<Escort>> Get(string partialName)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(EscortBLL.ListEscortsByPartialName(partialName),serializerSettings);
        }

        // GET api/values/5
        public JsonResult<Escort> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(EscortBLL.getEscortByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string escort)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            Escort u = JsonConvert.DeserializeObject<Escort>(escort, serializerSettings);
            return EscortBLL.InsertEscort(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string escort)
        {
            Escort u = JsonConvert.DeserializeObject<Escort>(escort);
            u.ID = id;
            return new { success = EscortBLL.UpdateEscort(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = EscortBLL.DeleteEscortByID(id) };
        }
    }
}
