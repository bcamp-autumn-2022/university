using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Course
    {
        public int idcourse { get; set; }
        public string name { get; set; }
        public Int16 greditpoints { get; set; }
        internal Database Db { get; set; }

        public Course()
        {
        }

        internal Course(Database db)
        {
            Db = db;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  course ;";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Course> FindOneAsync(int idcourse)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  course  WHERE  idcourse  = @idcourse";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idcourse",
                DbType = DbType.Int32,
                Value = idcourse,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO  course  (idcourse, name, greditpoints) VALUES (@idcourse, @name, @greditpoints);";
            BindId(cmd);
            BindParams(cmd);
            try
            {
                int affected=await cmd.ExecuteNonQueryAsync();
                return affected; 
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  course  SET  idcourse  = @idcourse,  name  = @name,  greditpoints  = @greditpoints WHERE  idcourse  = @idcourse;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  course  WHERE  idcourse  = @idcourse;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Course>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Course>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Course(Db)
                    {
                        idcourse = reader.GetInt32(0),
                        name = reader.GetString(1),
                        greditpoints = reader.GetInt16(2),
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
                ParameterName = "@idcourse",
                DbType = DbType.Int32,
                Value = idcourse,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@greditpoints",
                DbType = DbType.Int16,
                Value = greditpoints,
            });
        }
    }
}