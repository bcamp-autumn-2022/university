using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace university
{
    public class Grade
    {
        public int idgrade { get; set; }
        public DateTime date { get; set; }
        public int idstudent { get; set; }
        public int idteacher { get; set; }
        public int idcourse { get; set; }
        public int grade { get; set; }

        internal Database Db { get; set; }

        public Grade()
        {
        }

        internal Grade(Database db)
        {
            Db = db;
        }

        public async Task<List<Grade>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  grade ;";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Grade> FindOneAsync(int idgrade)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  grade  WHERE  idgrade  = @idgrade";
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@idgrade",
                DbType = DbType.Int32,
                Value = idgrade,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO  grade  ( date,idstudent,idteacher,idcourse,grade ) VALUES (@date,@idstudent,@idteacher,@idcourse,@grade);";
            BindParams(cmd);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                int lastInsertId = 1;
                return lastInsertId; 
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  grade  SET  date  = @date,  idstudent  = @idstudent,  idteacher  = @idteacher, idcourse=@idcourse, grade=@grade WHERE  idgrade  = @idgrade;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  grade  WHERE  idgrade  = @idgrade;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Grade>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Grade>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Grade(Db)
                    {
                        idgrade = reader.GetInt32(0),
                        date = reader.GetDateTime(1),
                        idstudent = reader.GetInt32(2),
                        idteacher = reader.GetInt32(3),
                        idcourse = reader.GetInt32(4),
                        grade = reader.GetInt32(5)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        private void BindId(NpgsqlCommand cmd)
        {
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@idgrade",
                DbType = DbType.Int32,
                Value = idgrade,
            });
        }

        private void BindParams(NpgsqlCommand cmd)
        {
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@date",
                DbType = DbType.DateTime,
                Value = date,
            });
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@idstudent",
                DbType = DbType.Int32,
                Value = idstudent,
            });
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@idteacher",
                DbType = DbType.Int32,
                Value = idteacher,
            });
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@idcourse",
                DbType = DbType.Int32,
                Value = idcourse,
            });
            cmd.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "@grade",
                DbType = DbType.Int32,
                Value = grade,
            });
        }
    }
}