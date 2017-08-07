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
    public class PaymentMethodDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        static string SQL_TABLE_FIELDS = @"
            ID_User
           ,ID_Pay_Type
           ,ID_Credit_Card
           ,Active
           ,Reg_Date";

        static string SQL_TABLE_METADATA = @"
            @ID_User
           ,@ID_Pay_Type
           ,@ID_Credit_Card
           ,@Active
           ,@Reg_Date";

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO PaymentMethod
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
            UPDATE Payment_Method
            SET  
            ID_User=@ID_User
           ,ID_Pay_Type=@ID_Pay_Type
           ,ID_Credit_Card=@ID_Credit_Card
           ,Active=@Active
           ,Reg_Date=@Reg_Date
            WHERE ID = @ID
";


        #endregion

        #region SQL GET TIME PRICE BY ID

        static string SQL_GET_PAYMENT_METHOD_BY_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS+
            @"
        FROM Payment_Method
        WHERE ID = @ID
        ";

        #endregion

        #region GET PAYMENT_METHOD BY ID USER OR SCORT

        static string SQL_GET_PAYMENT_METHOD_BY_ESCORT_ID = @"
        SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Payment_Method
        WHERE ID_Escort = @ID
        ";

        #endregion

        #region GET TIME PRICES

        static string SQL_GET_PAYMENT_METHODS = @"
            SELECT
            ID,"
            +
            SQL_TABLE_FIELDS +
            @"
        FROM Payment_Method
";

        #endregion

        #region DELETE PAYMENT_METHOD BY ID

        static string DELETE_PAYMENT_METHOD_BY_ID = @"

            DELETE FROM Payment_Method 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public PaymentMethodDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertPaymentMethod(PaymentMethod paymentMethod)
        {
            DynamicParameters parameters = new DynamicParameters();
            int active = paymentMethod.Active.HasValue ?
                Convert.ToInt32(paymentMethod.Active.Value) : 0;
            parameters.Add("@ID_User", paymentMethod.ID_User, DbType.Int32);
            parameters.Add("@ID_Pay_Type", paymentMethod.ID_Pay_Type, DbType.Int32);
            parameters.Add("@ID_Credit_Card", paymentMethod.ID_Credit_Card, DbType.Int32);
            parameters.Add("@Active", active, DbType.Int32);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.Int32);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            int active = paymentMethod.Active.HasValue ?
                Convert.ToInt32(paymentMethod.Active.Value) : 0;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_User", paymentMethod.ID_User, DbType.Int32);
            parameters.Add("@ID_Pay_Type", paymentMethod.ID_Pay_Type, DbType.Int32);
            parameters.Add("@ID_Credit_Card", paymentMethod.ID_Credit_Card, DbType.Int32);
            parameters.Add("@Active", active, DbType.Int32);
            parameters.Add("@ID", paymentMethod.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public PaymentMethod getPaymentMethodByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<PaymentMethod>(connection, SQL_GET_PAYMENT_METHOD_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeletePaymentMethodByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_PAYMENT_METHOD_BY_ID, parameters) > 0;
        }

        public List<PaymentMethod> ListPaymentMethodByEscortID(int escortID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", escortID, DbType.Int32);
            return SqlMapper.Query<PaymentMethod>(connection, SQL_GET_PAYMENT_METHOD_BY_ESCORT_ID, parameters).ToList();
        }

        public List<PaymentMethod> ListPaymentMethods()
        {
            return SqlMapper.Query<PaymentMethod>(connection, SQL_GET_PAYMENT_METHODS).ToList();
        }
    }
}
