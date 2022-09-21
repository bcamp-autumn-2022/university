using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Student
    {
        public int idstudent { get; set; }
        public DateTime start_date { get; set; }
        public DateTime graduate_date { get; set; }

        internal Database Db { get; set; }

        public Student()
        {
        }

        internal Student(Database db)
        {
            Db = db;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  student ;";
            var result=await ReturnAllAsync(await cmd.ExecuteReaderAsync());
           // Console.WriteLine(result);
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Student> FindOneAsync(int idstudent)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  student  WHERE  idstudent  = @idstudent";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            Console.WriteLine(result.Count);
            if(result.Count > 0){
                return result[0];
            }
            else {
                return null;
            }
            //return result.Count > 0 ? result[0] : null;
        }


        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  student ";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }
    

        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText=@"insert into student(idstudent,start_date,graduate_date) 
            values(@idstudent,@start_date,@graduate_date);";
            BindParams(cmd);
            BindId(cmd);
            try
            {
                int affectedRows=await cmd.ExecuteNonQueryAsync();
                return affectedRows;
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  student  SET  idstudent = @idstudent, start_date = @start_date, graduate_date = @graduate_date WHERE  idstudent  = @idstudent;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  student  WHERE  idstudent  = @idstudent;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Student>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Student>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Student(Db)
                    {
                        idstudent = reader.GetInt32(0),
                        start_date = reader.GetDateTime(1),
                        graduate_date = reader.GetDateTime(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

         private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.String,
                Value = idstudent,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@start_date",
                DbType = DbType.DateTime,
                Value = start_date,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@graduate_date",
                DbType = DbType.DateTime,
                Value = graduate_date,
            });
        }
    }
}