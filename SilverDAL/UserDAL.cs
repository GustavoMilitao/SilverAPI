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
    public class UserDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO Users
           (Name
           ,Address
           ,City
           ,State
           ,Addresscode
           ,Country
           ,DDI
           ,DDD
           ,Phonenumber
           ,Nickname
           ,Password
           ,Email
           ,Reg_Date)
     OUTPUT INSERTED.ID
     VALUES
           (@Name
           ,@Address
           ,@City
           ,@State
           ,@Addresscode
           ,@Country
           ,@DDI
           ,@DDD
           ,@Phonenumber
           ,@Nickname
           ,CONVERT(BINARY, @Password)
           ,@Email
           ,@Reg_Date)
";

        #endregion

        #region SQL UPDATE

        static string SQL_UPDATE = @"
            UPDATE Users
            SET  Name = @Name
                ,Address = @Address
                ,City = @City
                ,State = @State
                ,Addresscode = @Addresscode
                ,Country = @Country
                ,DDI = @DDI
                ,DDD = @DDD
                ,Phonenumber = @Phonenumber
                ,Nickname = @Nickname
                ,Password = CONVERT(BINARY,@Password)
                ,Email = @Email
            WHERE ID = @ID

";


        #endregion

        #region SQL GET USER BY ID

        static string SQL_GET_USER_BY_ID = @"
        SELECT
            ID
           ,Name
           ,Address
           ,City
           ,State
           ,Addresscode
           ,Country
           ,DDI
           ,DDD
           ,Phonenumber
           ,Nickname
           ,ISNULL(CONVERT(VARCHAR,Password),'') Password
           ,Email
           ,Reg_Date
        FROM Users
        WHERE ID = @ID
        ";

        #endregion

        #region GET USER BY PARTIAL NAME

        static string SQL_GET_USER_BY_PARTIAL_NAME = @"
        SELECT
            ID
           ,Name
           ,Address
           ,City
           ,State
           ,Addresscode
           ,Country
           ,DDI
           ,DDD
           ,Phonenumber
           ,Nickname
           ,ISNULL(CONVERT(VARCHAR,Password),'') Password
           ,Email
           ,Reg_Date
        FROM Users
        WHERE Name LIKE @Name OR Nickname LIKE @Nickname
        ";

        #endregion

        #region GET USER BY PARTIAL NICKNAME

        static string SQL_GET_USER_BY_PARTIAL_NICKNAME = @"
        SELECT
            ID
           ,Name
           ,Address
           ,City
           ,State
           ,Addresscode
           ,Country
           ,DDI
           ,DDD
           ,Phonenumber
           ,Nickname
           ,ISNULL(CONVERT(VARCHAR,Password),'') Password
           ,Email
           ,Reg_Date
        FROM Users
        WHERE Nickname LIKE @Nickname
        ";

        #endregion

        #region GET USERS

        static string SQL_GET_USERS = @"
            SELECT
            ID
           ,Name
           ,Address
           ,City
           ,State
           ,Addresscode
           ,Country
           ,DDI
           ,DDD
           ,Phonenumber
           ,Nickname
           ,ISNULL(CONVERT(VARCHAR,Password),'') Password
           ,Email
           ,Reg_Date
        FROM Users
";

        #endregion

        #region DELETE USER BY ID

        static string DELETE_USER_BY_ID = @"

            DELETE FROM Users 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public UserDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertUser(User user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", user.Name, DbType.AnsiString);
            parameters.Add("@Address", user.Address, DbType.AnsiString);
            parameters.Add("@City", user.City, DbType.AnsiString);
            parameters.Add("@State", user.State, DbType.AnsiString);
            parameters.Add("@Addresscode", user.Addresscode, DbType.AnsiString);
            parameters.Add("@Country", user.Country, DbType.AnsiString);
            parameters.Add("@DDI", user.DDI, DbType.AnsiString);
            parameters.Add("@DDD", user.DDD, DbType.AnsiString);
            parameters.Add("@Phonenumber", user.Phonenumber, DbType.AnsiString);
            parameters.Add("@Nickname", user.Nickname, DbType.AnsiString);
            parameters.Add("@Password", user.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@Email", user.Email, DbType.AnsiString);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdateUser(User user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", user.Name, DbType.AnsiString);
            parameters.Add("@Address", user.Address, DbType.AnsiString);
            parameters.Add("@City", user.City, DbType.AnsiString);
            parameters.Add("@State", user.State, DbType.AnsiString);
            parameters.Add("@Addresscode", user.Addresscode, DbType.AnsiString);
            parameters.Add("@Country", user.Country, DbType.AnsiString);
            parameters.Add("@DDI", user.DDI, DbType.AnsiString);
            parameters.Add("@DDD", user.DDD, DbType.AnsiString);
            parameters.Add("@Phonenumber", user.Phonenumber, DbType.AnsiString);
            parameters.Add("@Nickname", user.Nickname, DbType.AnsiString);
            parameters.Add("@Password", user.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@Email", user.Email, DbType.AnsiString);
            parameters.Add("@ID", user.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public User getUserByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<User>(connection, SQL_GET_USER_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeleteUserByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_USER_BY_ID, parameters) > 0;
        }

        public List<User> ListUsersByPartialName(string partialName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", "%" + partialName + "%", DbType.AnsiString);
            parameters.Add("@Nickname", "%" + partialName + "%", DbType.AnsiString);
            return SqlMapper.Query<User>(connection, SQL_GET_USER_BY_PARTIAL_NAME, parameters).ToList();
        }

        public List<User> ListUsers()
        {
            return SqlMapper.Query<User>(connection, SQL_GET_USERS).ToList();
        }
    }
}
