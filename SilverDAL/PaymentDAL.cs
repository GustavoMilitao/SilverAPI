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
    public class PaymentDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        static string SQL_TABLE_FIELDS = @"
            ID_Pay_Method
           ,ID_Meeting
           ,Value
           ,Reg_Date";

        static string SQL_TABLE_METADATA = @"
            @ID_Pay_Method
           ,@ID_Meeting
           ,@Value
           ,@Reg_Date";

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO Payment
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
            UPDATE Payment
            SET  

            ID_Pay_Method=@ID_Pay_Method
           ,ID_Meeting=@ID_Meeting
           ,Value=@Value
            WHERE ID = @ID
";


        #endregion

        #region SQL GET TIME PRICE BY ID

        static string SQL_GET_PAYMENT_BY_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS+
            @"
        FROM Payment
        WHERE ID = @ID
        ";

        #endregion

        #region GET PAYMENT BY ID USER OR SCORT

        static string SQL_GET_PAYMENT_BY_ESCORT_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Payment
        WHERE ID_Escort = @ID
        ";

        #endregion

        #region GET TIME PRICES

        static string SQL_GET_PAYMENTS = @"
            SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Payment
";

        #endregion

        #region DELETE PAYMENT BY ID

        static string DELETE_PAYMENT_BY_ID = @"

            DELETE FROM Payment 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public PaymentDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertPayment(Payment payment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Pay_Method", payment.ID_Pay_Method, DbType.Int32);
            parameters.Add("@ID_Meeting", payment.ID_Meeting, DbType.Int32);
            parameters.Add("@Value", payment.Value, DbType.Decimal);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdatePayment(Payment payment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Pay_Method", payment.ID_Pay_Method, DbType.Int32);
            parameters.Add("@ID_Meeting", payment.ID_Meeting, DbType.Int32);
            parameters.Add("@Value", payment.Value, DbType.Decimal);
            parameters.Add("@ID", payment.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public Payment getPaymentByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<Payment>(connection, SQL_GET_PAYMENT_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeletePaymentByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_PAYMENT_BY_ID, parameters) > 0;
        }

        public List<Payment> ListPaymentByEscortID(int escortID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", escortID, DbType.Int32);
            return SqlMapper.Query<Payment>(connection, SQL_GET_PAYMENT_BY_ESCORT_ID, parameters).ToList();
        }

        public List<Payment> ListPayments()
        {
            return SqlMapper.Query<Payment>(connection, SQL_GET_PAYMENTS).ToList();
        }
    }
}
