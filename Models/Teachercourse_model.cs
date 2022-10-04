using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Teachercourse
    {
        //Teacher properties
        public string? Teachername { get; set; }
        public string? Username { get; set; }
        public string? Department { get; set; }
        public string? Course { get; set; }

        internal Database Db { get; set; }
        public Teachercourse()
        {
        }

        internal Teachercourse(Database db)
        {
            Db = db;
        }

        public async Task<List<Teachercourse>> GetAllTeacherCoursesAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select concat(firstname, ' ',lastname) as 'techer_name', username, 
            department.name as 'department', course.name as 'course'
            from user inner join teacher on iduser=teacher.idteacher 
            inner join department on teacher.iddepartment=department.iddepartment 
            inner join grade on teacher.idteacher=grade.idteacher 
            inner join course on course.idcourse=grade.idcourse;";
            return await ReturnTeachersAsync(await cmd.ExecuteReaderAsync());
        }

         public async Task<List<Teachercourse>> GetOneTeacherCoursesAsync(int idTeacher)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select concat(firstname, ' ',lastname) as 'techer_name', username, 
            department.name as 'department', course.name as 'course'
            from user inner join teacher on iduser=teacher.idteacher 
            inner join department on teacher.iddepartment=department.iddepartment 
            inner join grade on teacher.idteacher=grade.idteacher 
            inner join course on course.idcourse=grade.idcourse where teacher.idTeacher=@idTeacher;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idTeacher",
                DbType = DbType.Int32,
                Value = idTeacher,
            });
            return await ReturnTeachersAsync(await cmd.ExecuteReaderAsync());
        } 

        private async Task<List<Teachercourse>> ReturnTeachersAsync(DbDataReader reader)
        {
            var posts = new List<Teachercourse>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Teachercourse(Db)
                    {
                        Teachername = reader.GetString(0),
                        Username = reader.GetString(1),
                        Department = reader.GetString(2),
                        Course = reader.GetString(3)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

       

    }
}