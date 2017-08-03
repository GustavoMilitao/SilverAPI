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
    public class BankingAccountDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO Banking_Account
           (ID_Escort
           ,Bankcode
           ,Bankname
           ,Agency
           ,Current_Account
           ,Digit
           ,Reg_Date)
     OUTPUT INSERTED.ID
     VALUES
           (@ID_Escort
           ,@Bankcode
           ,@Bankname
           ,@Agency
           ,@Current_Account
           ,@Digit
           ,@Reg_Date)
";

        #endregion

        #region SQL UPDATE

        static string SQL_UPDATE = @"
            UPDATE Banking_Account
            SET 
               ,ID_Escort = @ID_Escort
               ,Bankcode = @Bankcode
               ,Bankname = @Bankname
               ,Agency = @Agency
               ,Current_Account = @Current_Account
               ,Digit = @Digit    
               ,Reg_Date = @Reg_Date
            WHERE ID = @ID

";


        #endregion

        #region SQL GET Banking Account BY ID

        static string SQL_GET_BANKING_ACCOUNT_BY_ID = @"
        SELECT
            ID
           ,ID_Escort
           ,Bankcode
           ,Bankname
           ,Agency
           ,Current_Account
           ,Digit
           ,Reg_Date
        FROM Banking_Account
        WHERE ID = @ID
        ";

        #endregion

        #region GET Banking Account BY PARTIAL NAME

        static string SQL_GET_BANKING_ACCOUNT_BY_PARTIAL_BANKNAME = @"
        SELECT
            ID
           ,ID_Escort
           ,Bankcode
           ,Bankname
           ,Agency
           ,Current_Account
           ,Digit
           ,Reg_Date
        FROM Banking_Account
        WHERE Bankname LIKE @Bankname
        ";

        #endregion

        #region GET BankingAccount

        static string SQL_GET_BANKING_ACCOUNT = @"
        SELECT
            ID
           ,ID_Escort
           ,Bankcode
           ,Bankname
           ,Agency
           ,Current_Account
           ,Digit
           ,Reg_Date
        FROM Banking_Account
";

        #endregion

        #region DELETE BankingAccount BY ID

        static string DELETE_BANKING_ACCOUNT_BY_ID = @"

            DELETE FROM Banking_Account 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public BankingAccountDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertBankingAccount(BankingAccount bankingAccount)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Escort", bankingAccount.ID_Escort, DbType.AnsiString);
            parameters.Add("@Bankcode", bankingAccount.Bankcode, DbType.AnsiString);
            parameters.Add("@Bankname", bankingAccount.Bankname, DbType.AnsiString);
            parameters.Add("@Agency", bankingAccount.Agency, DbType.AnsiString);
            parameters.Add("@Current_Account", bankingAccount.Current_Account, DbType.AnsiString);
            parameters.Add("@Digit", bankingAccount.Digit, DbType.AnsiString);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdateBankingAccount(BankingAccount bankingAccount)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID_Escort", bankingAccount.ID_Escort, DbType.AnsiString);
            parameters.Add("@Bankcode", bankingAccount.Bankcode, DbType.AnsiString);
            parameters.Add("@Bankname", bankingAccount.Bankname, DbType.AnsiString);
            parameters.Add("@Agency", bankingAccount.Agency, DbType.AnsiString);
            parameters.Add("@Current_Account", bankingAccount.Current_Account, DbType.AnsiString);
            parameters.Add("@Digit", bankingAccount.Digit, DbType.AnsiString);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);
            parameters.Add("@ID", bankingAccount.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public BankingAccount getBankingAccountByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<BankingAccount>(connection, SQL_GET_BANKING_ACCOUNT_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeleteBankingAccountByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_BANKING_ACCOUNT_BY_ID, parameters) > 0;
        }

        public List<BankingAccount> ListBankingAccountsByPartialBankname(string partialName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Bankname", "%" + partialName + "%", DbType.AnsiString);
            return SqlMapper.Query<BankingAccount>(connection, SQL_GET_BANKING_ACCOUNT_BY_PARTIAL_BANKNAME, parameters).ToList();
        }

        public List<BankingAccount> ListBankingAccounts()
        {
            return SqlMapper.Query<BankingAccount>(connection, SQL_GET_BANKING_ACCOUNT).ToList();
        }
    }
}
