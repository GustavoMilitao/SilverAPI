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
    public class EscortDAL
    {
        static string connectionString;
        static SqlConnection connection;


        #region SQL

        #region SQL_INSERIR

        static string SQL_INSERIR = @"

INSERT INTO Escort
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
           ,Description
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
           ,@Description    
           ,@Email
           ,@Reg_Date)
";

        #endregion

        #region SQL UPDATE

        static string SQL_UPDATE = @"
            UPDATE Escort
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
                ,Description = @Description
                ,Email = @Email
            WHERE ID = @ID

";


        #endregion

        #region SQL GET Escort BY ID

        static string SQL_GET_ESCORT_BY_ID = @"
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
           ,Description
           ,Email
           ,Reg_Date
        FROM Escort
        WHERE ID = @ID
        ";

        #endregion

        #region GET Escort BY PARTIAL NAME

        static string SQL_GET_ESCORT_BY_PARTIAL_NAME = @"
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
           ,Description
           ,Email
           ,Reg_Date
        FROM Escort
        WHERE Name LIKE @Name OR Nickname LIKE @Nickname
        ";

        #endregion

        #region GET Escort BY PARTIAL NICKNAME

        static string SQL_GET_ESCORT_BY_PARTIAL_NICKNAME = @"
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
           ,Description
           ,Email
           ,Reg_Date
        FROM Escort
        WHERE Nickname LIKE @Nickname
        ";

        #endregion

        #region GET Escort

        static string SQL_GET_ESCORT = @"
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
           ,Description
           ,Email
           ,Reg_Date
        FROM Escort
";

        #endregion

        #region DELETE Escort BY ID

        static string DELETE_ESCORT_BY_ID = @"

            DELETE FROM Escort 
            WHERE ID = @ID
";

        #endregion

        #endregion
        public EscortDAL()
        {
            connectionString = ConfigurationManager.AppSettings["connectionStringSilver"];
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public int InsertEscort(Escort escort)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", escort.Name, DbType.AnsiString);
            parameters.Add("@Address", escort.Address, DbType.AnsiString);
            parameters.Add("@City", escort.City, DbType.AnsiString);
            parameters.Add("@State", escort.State, DbType.AnsiString);
            parameters.Add("@Addresscode", escort.Addresscode, DbType.AnsiString);
            parameters.Add("@Country", escort.Country, DbType.AnsiString);
            parameters.Add("@DDI", escort.DDI, DbType.AnsiString);
            parameters.Add("@DDD", escort.DDD, DbType.AnsiString);
            parameters.Add("@Phonenumber", escort.Phonenumber, DbType.AnsiString);
            parameters.Add("@Nickname", escort.Nickname, DbType.AnsiString);
            parameters.Add("@Password", escort.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@Description", escort.Description, DbType.AnsiString);
            parameters.Add("@Email", escort.Email, DbType.AnsiString);

            return (int) SqlMapper.ExecuteScalar(connection, SQL_INSERIR, parameters);
        }

        public bool UpdateEscort(Escort escort)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", escort.Name, DbType.AnsiString);
            parameters.Add("@Address", escort.Address, DbType.AnsiString);
            parameters.Add("@City", escort.City, DbType.AnsiString);
            parameters.Add("@State", escort.State, DbType.AnsiString);
            parameters.Add("@Addresscode", escort.Addresscode, DbType.AnsiString);
            parameters.Add("@Country", escort.Country, DbType.AnsiString);
            parameters.Add("@DDI", escort.DDI, DbType.AnsiString);
            parameters.Add("@DDD", escort.DDD, DbType.AnsiString);
            parameters.Add("@Phonenumber", escort.Phonenumber, DbType.AnsiString);
            parameters.Add("@Nickname", escort.Nickname, DbType.AnsiString);
            parameters.Add("@Password", escort.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@Description", escort.Description, DbType.AnsiString);
            parameters.Add("@Email", escort.Email, DbType.AnsiString);
            parameters.Add("@Reg_Date", DateTime.Now, DbType.DateTime);
            parameters.Add("@ID", escort.ID, DbType.Int32);

            return SqlMapper.Execute(connection, SQL_UPDATE, parameters) > 0;
        }

        public Escort getEscortByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Query<Escort>(connection, SQL_GET_ESCORT_BY_ID, parameters).FirstOrDefault();
        }

        public bool DeleteEscortByID(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            return SqlMapper.Execute(connection, DELETE_ESCORT_BY_ID, parameters) > 0;
        }

        public List<Escort> ListEscortsByPartialName(string partialName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", "%" + partialName + "%", DbType.AnsiString);
            parameters.Add("@Nickname", "%" + partialName + "%", DbType.AnsiString);
            return SqlMapper.Query<Escort>(connection, SQL_GET_ESCORT_BY_PARTIAL_NAME, parameters).ToList();
        }

        public List<Escort> ListEscorts()
        {
            return SqlMapper.Query<Escort>(connection, SQL_GET_ESCORT).ToList();
        }
    }
}
