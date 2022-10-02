using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Common
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Start_date { get; set; }
        public string? Gradua_date { get; set; }
        internal Database Db { get; set; }

        public Common()
        {
        }

        internal Common(Database db)
        {
            Db = db;
        }

        public async Task<List<Common>> GetAllStudentsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select firstname, lastname,username,
            date_format(start_date,'%d.%m.%Y') as 'start_date', 
            date_format(graduate_date,'%d.%m.%Y') as 'graduate_date' 
            from student inner join user on idstudent=iduser;";
            return await ReturnStudentsAsync(await cmd.ExecuteReaderAsync());
        }

         public async Task<Common> GetOneStudentAsync(int idstudent)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select firstname, lastname,username,
            date_format(start_date,'%d.%m.%Y') as 'start_date', 
            date_format(graduate_date,'%d.%m.%Y') as 'graduate_date' 
            from student inner join user on idstudent=iduser where idstudent=@idstudent;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
            var result = await ReturnStudentsAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        } 


        private async Task<List<Common>> ReturnStudentsAsync(DbDataReader reader)
        {
            var posts = new List<Common>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Common(Db)
                    {
                        Firstname = reader.GetString(0),
                        Lastname = reader.GetString(1),
                        Username = reader.GetString(2),
                        Start_date = reader.GetString(3),
                        Gradua_date = reader.GetString(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}