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
    public class UsersController : ApiController
    {
        private UserBLL userBLL;

        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }


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
        public JsonResult<List<User>> Get()
        {
            List<User> users = UserBLL.ListUsers();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(users, serializerSettings);
        }

        public JsonResult<List<User>> Get(string partialName)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(UserBLL.ListUsersByPartialName(partialName),serializerSettings);
        }

        // GET api/values/5
        public JsonResult<User> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(UserBLL.getUserByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string user)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            User u = JsonConvert.DeserializeObject<User>(user, serializerSettings);
            return UserBLL.InsertUser(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string user)
        {
            User u = JsonConvert.DeserializeObject<User>(user);
            u.ID = id;
            return new { success = UserBLL.UpdateUser(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = UserBLL.DeleteUserByID(id) };
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("users/{userID}/meetings")]
        public JsonResult<List<Meeting>> Meetings(int userID)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(MeetingBLL.ListMeetingByUserID(userID), serializerSettings);
        }
    }
}
