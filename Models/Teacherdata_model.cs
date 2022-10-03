using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Teacherdata
    {
        //Teacher properties
        public string? Teachername { get; set; }
        public string Username { get; set; }
        public string? Department { get; set; }

        internal Database Db { get; set; }
        public Teacherdata()
        {
        }

        internal Teacherdata(Database db)
        {
            Db = db;
        }

        public async Task<List<Teacherdata>> GetAllTeachersAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select concat(firstname, ' ',lastname) as 'techer_name', 
            username, name as 'department' from user inner join teacher on iduser=idteacher 
            inner join department on teacher.iddepartment=department.iddepartment;";
            return await ReturnTeachersAsync(await cmd.ExecuteReaderAsync());
        }

         public async Task<Teacherdata> GetOneTeacherAsync(int idTeacher)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select concat(firstname, ' ',lastname) as 'techer_name', 
            username, name as 'department' from user inner join teacher on iduser=idteacher 
            inner join department on teacher.iddepartment=department.iddepartment where idTeacher=@idTeacher;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idTeacher",
                DbType = DbType.Int32,
                Value = idTeacher,
            });
            var result = await ReturnTeachersAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        } 

        private async Task<List<Teacherdata>> ReturnTeachersAsync(DbDataReader reader)
        {
            var posts = new List<Teacherdata>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Teacherdata(Db)
                    {
                        Teachername = reader.GetString(0),
                        Username = reader.GetString(1),
                        Department = reader.GetString(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

       

    }
}