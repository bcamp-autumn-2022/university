using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace university
{
    public class Identity
    {

        public Int32? IdentityValue { get; set; }
        internal Database? Db { get; set; }
        public Identity()
        {
        }

        internal Identity(Database db)
        {
            Db = db;
        }

        public async Task<string> GetUserIdentity(string username)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select identity from user where username=@username";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = username,
            });
            var result = await ReturnIdentityAsync(await cmd.ExecuteReaderAsync());
           
            return result;
        }


        private async Task<string> ReturnIdentityAsync(DbDataReader reader)
        {
            var objectIdentity=new Identity();
            using (reader)
            {

                await reader.ReadAsync();
                objectIdentity.IdentityValue=reader.GetInt32(0);

            }

            return objectIdentity.IdentityValue.ToString();
        }



    }
}