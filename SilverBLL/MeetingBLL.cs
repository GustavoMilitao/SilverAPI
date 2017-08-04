using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class MeetingBLL
    {
        private MeetingDAL meetingDAL;

        public MeetingDAL MeetingDAL
        {
            get
            {
                if (meetingDAL == null)
                    meetingDAL = new MeetingDAL();
                return meetingDAL;
            }
        }

        public int InsertMeeting(Meeting meeting)
        {
            return MeetingDAL.InsertMeeting(meeting);
        }

        public bool UpdateMeeting(Meeting meeting)
        {
            return MeetingDAL.UpdateMeeting(meeting);
        }

        public Meeting getMeetingByID(int id)
        {
            return MeetingDAL.getMeetingByID(id);
        }

        public bool DeleteMeetingByID(int id)
        {
            return MeetingDAL.DeleteMeetingByID(id);
        }

        public List<Meeting> ListMeetingByUserID(int userID)
        {
            return MeetingDAL.ListMeetingByUserID(userID);
        }

        public List<Meeting> ListMeetingByEscortID(int escortID)
        {
            return MeetingDAL.ListMeetingByEscortID(escortID);
        }

        public List<Meeting> ListMeetings()
        {
            return MeetingDAL.ListMeetings();
        }
    }
}
