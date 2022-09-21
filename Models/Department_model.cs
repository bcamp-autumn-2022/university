using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace university
{
    public class Department
    {
        public int iddepartment { get; set; }
        public string name { get; set; }

        internal Database Db { get; set; }

        public Department() {}

        internal Department(Database db)
        {
            Db = db;
        }

        public async Task<List<Department>> GetAllColumns()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM department";
            return await ReturnAll(await cmd.ExecuteReaderAsync());
        }

        public async Task<Department> FindOneColumn(int iddepartment)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM department
            WHERE iddepartment = @iddepartment";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@iddepartment",
                DbType = DbType.Int32,
                Value = iddepartment,
            });
            var result = await ReturnAll(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO department (iddepartment, name)
            VALUES (@iddepartment, @name)";
            BindId(cmd);
            BindParams(cmd);
            try
            {
                int value = await cmd.ExecuteNonQueryAsync();
                return value;
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE department SET name = @name WHERE iddepartment = @iddepartment";
            BindId(cmd);
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM department WHERE iddepartment = @iddepartment";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Department>> ReturnAll(DbDataReader reader)
        {
            var posts = new List<Department>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Department(Db)
                    {
                        iddepartment = reader.GetInt32(0),
                        name = reader.GetString(1)
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
                ParameterName = "@iddepartment",
                DbType = DbType.Int32,
                Value = iddepartment,
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
        }
    }
}