using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Studentgrade
    {


        //grade properties
        public string? Coursename { get; set; }
        public Int16? Grade { get; set; }
        public Int16? Greditpoint { get; set; }
        public string? Gradedate { get; set; }
        public string? Teacher { get; set; }
        //database
        internal Database? Db { get; set; }

        public Studentgrade()
        {
        }

        internal Studentgrade(Database db)
        {
            Db = db;
        }
        
        public async Task<List<Studentgrade>> GetStudentGrades()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select name as 'course name',grade,greditpoints,date_format(date,'%d.%m.%Y') as 'date', concat(firstname,' ', lastname) as 'teacher' from course inner join grade on course.idcourse=grade.idcourse
                inner join teacher on teacher.idteacher=grade.idteacher inner join user on iduser=teacher.idteacher ;";

            var result = await ReturnGradesAsync(await cmd.ExecuteReaderAsync());
            return await ReturnGradesAsync(await cmd.ExecuteReaderAsync());
        }
        public async Task<Studentgrade> GetOneStudentGrades(int idstudent)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select name as 'course name',grade,greditpoints,date_format(date,'%d.%m.%Y') as 'date', concat(firstname,' ', lastname) as 'teacher' from course inner join grade on course.idcourse=grade.idcourse
                inner join teacher on teacher.idteacher=grade.idteacher inner join user on iduser=teacher.idteacher where idstudent=@idstudent;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
            var result = await ReturnGradesAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        } 
        

        private async Task<List<Studentgrade>> ReturnGradesAsync(DbDataReader reader)
        {
            var posts = new List<Studentgrade>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Studentgrade(Db)
                    {
                        Coursename = reader.GetString(0),
                        Grade = reader.GetInt16(1),
                        Greditpoint = reader.GetInt16(2),
                        Gradedate = reader.GetString(3),
                        Teacher = reader.GetString(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }


    }
}