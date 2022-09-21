using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Login
    {
        //public string username { get; set; }
        public string password { get; set; }
        public int identity { get; set; }

        internal Database Db { get; set; }

        public Login()
        {
        }

        internal Login(Database db)
        {
            Db = db;
        }


        public async Task<Login> GetPassword(string username)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  password, identity   FROM  user  WHERE  username  = @username";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value =  username,
            });
            var result = await ReturnPassword(await cmd.ExecuteReaderAsync());
            return result;
        }

        private async Task<Login> ReturnPassword(DbDataReader reader)
        {
            var objectLogin = new Login();
            using (reader)
            {
                await reader.ReadAsync();
                objectLogin.password=reader.GetString(0);
                objectLogin.identity=reader.GetInt32(1);

            }

            return objectLogin;
        }
        
    
    }
}