using Dapper;
using SilverEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverDAL
{
    public class MeetingDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        static string SQL_TABLE_FIELDS = @"
            ID_User
           ,ID_Escort
           ,Total_Time_In_Hours
           ,Total_Price
           ,Reg_Date";

        static string SQL_TABLE_METADATA = @"
            @ID_User
           ,@ID_Escort
           ,@Total_Time_In_Hours
           ,@Total_Price
           ,@Reg_Date
";

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO Meeting
           ("+
            SQL_TABLE_FIELDS+
            @")
     OUTPUT INSERTED.ID
     VALUES
           ("+
            SQL_TABLE_METADATA+
            @")
";

        #endregion

        #region SQL UPDATE

        static string SQL_UPDATE = @"
            UPDATE Meeting
            SET  
            ID_User                 = @ID_User               
           ,ID_Escort               = @ID_Escort            
           ,Total_Time_In_Hours     = @Total_Time_In_Hours  
           ,Total_Price             = @Total_Price          
            WHERE ID = @ID
";


        #endregion

        #region SQL GET MEETING BY ID

        static string SQL_GET_MEETING_BY_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS+
            @"
        FROM Meeting
        WHERE ID = @ID
        ";

        #endregion

        #region GET MEETING BY ID USER OR SCORT

        static string SQL_GET_MEETING_BY_USER_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Meeting
        WHERE ID_User = @ID
        ";

        static string SQL_GET_MEETING_BY_ESCORT_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Meeting
        WHERE ID_Escort = @ID
        ";

        #endregion


        #region GET MEETINGS

        static string SQL_GET_MEETINGS = @"
            SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Meeting
";

        #endregion

        #region DELETE MEETING BY ID

        static string DELETE_MEETING_BY_ID = @"

            DELETE FROM Meeting 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public MeetingDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertMeeting(Meeting meeting)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_User", meeting.ID_User, DbType.Int32);
            parameters.Add("@ID_Escort", meeting.ID_Escort, DbType.Int32);
            parameters.Add("@Total_Time_In_Hours", meeting.Total_Time_In_Hours, DbType.Int32);
            parameters.Add("@Total_Price", meeting.Total_Price, DbType.Decimal);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdateMeeting(Meeting meeting)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_User", meeting.ID_User, DbType.Int32);
            parameters.Add("@ID_Escort", meeting.ID_Escort, DbType.Int32);
            parameters.Add("@Total_Time_In_Hours", meeting.Total_Time_In_Hours, DbType.Int32);
            parameters.Add("@Total_Price", meeting.Total_Price, DbType.Decimal);
            parameters.Add("@ID", meeting.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public Meeting getMeetingByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<Meeting>(connection, SQL_GET_MEETING_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeleteMeetingByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_MEETING_BY_ID, parameters) > 0;
        }

        public List<Meeting> ListMeetingByUserID(int userID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", userID, DbType.Int32);
            return SqlMapper.Query<Meeting>(connection, SQL_GET_MEETING_BY_USER_ID, parameters).ToList();
        }

        public List<Meeting> ListMeetingByEscortID(int escortID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", escortID, DbType.Int32);
            return SqlMapper.Query<Meeting>(connection, SQL_GET_MEETING_BY_ESCORT_ID, parameters).ToList();
        }

        public List<Meeting> ListMeetings()
        {
            return SqlMapper.Query<Meeting>(connection, SQL_GET_MEETINGS).ToList();
        }
    }
}
