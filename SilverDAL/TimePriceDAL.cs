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
    public class TimePriceDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        static string SQL_TABLE_FIELDS = @"
            ID_Escort
           ,Time_In_Hour
           ,Price
           ,Reg_Date";

        static string SQL_TABLE_METADATA = @"
            @ID_Escort
           ,@Time_In_Hour
           ,@Price
           ,@Reg_Date
";

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO TimePrice
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
            UPDATE TimePrice
            SET  
           ,ID_Escort               = @ID_Escort            
           ,Time_In_Hour     = @Time_In_Hour  
           ,Price             = @Price          
            WHERE ID = @ID
";


        #endregion

        #region SQL GET TIME PRICE BY ID

        static string SQL_GET_TIME_PRICE_BY_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS+
            @"
        FROM TimePrice
        WHERE ID = @ID
        ";

        #endregion

        #region GET TIME_PRICE BY ID USER OR SCORT

        static string SQL_GET_TIME_PRICE_BY_ESCORT_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM TimePrice
        WHERE ID_Escort = @ID
        ";

        #endregion

        #region GET TIME PRICES

        static string SQL_GET_TIME_PRICES = @"
            SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM TimePrice
";

        #endregion

        #region DELETE TIME_PRICE BY ID

        static string DELETE_TIME_PRICE_BY_ID = @"

            DELETE FROM TimePrice 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public TimePriceDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertTimePrice(TimePrice timePrice)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Escort", timePrice.ID_Escort, DbType.Int32);
            parameters.Add("@Total_Time_In_Hours", timePrice.Time_In_Hour, DbType.Int32);
            parameters.Add("@Total_Price", timePrice.Price, DbType.Decimal);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdateTimePrice(TimePrice timePrice)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Escort", timePrice.ID_Escort, DbType.Int32);
            parameters.Add("@Total_Time_In_Hours", timePrice.Time_In_Hour, DbType.Int32);
            parameters.Add("@Total_Price", timePrice.Price, DbType.Decimal);
            parameters.Add("@ID", timePrice.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public TimePrice getTimePriceByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<TimePrice>(connection, SQL_GET_TIME_PRICE_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeleteTimePriceByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_TIME_PRICE_BY_ID, parameters) > 0;
        }

        public List<TimePrice> ListTimePriceByEscortID(int escortID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", escortID, DbType.Int32);
            return SqlMapper.Query<TimePrice>(connection, SQL_GET_TIME_PRICE_BY_ESCORT_ID, parameters).ToList();
        }

        public List<TimePrice> ListTimePrices()
        {
            return SqlMapper.Query<TimePrice>(connection, SQL_GET_TIME_PRICES).ToList();
        }
    }
}
