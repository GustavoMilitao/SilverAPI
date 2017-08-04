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
    public class MeetingsController : ApiController
    {
        private MeetingBLL meetingBLL;

        public MeetingBLL MeetingBLL
        {
            get
            {
                if (meetingBLL == null)
                    meetingBLL = new MeetingBLL();
                return meetingBLL;
            }
        }

        // GET api/values
        public JsonResult<List<Meeting>> Get()
        {
            List<Meeting> meetings = MeetingBLL.ListMeetings();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(meetings, serializerSettings);
        }

        // GET api/values/5
        public JsonResult<Meeting> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(MeetingBLL.getMeetingByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string meeting)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            Meeting u = JsonConvert.DeserializeObject<Meeting>(meeting, serializerSettings);
            return MeetingBLL.InsertMeeting(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string meeting)
        {
            Meeting u = JsonConvert.DeserializeObject<Meeting>(meeting);
            u.ID = id;
            return new { success = MeetingBLL.UpdateMeeting(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = MeetingBLL.DeleteMeetingByID(id) };
        }
    }
}
