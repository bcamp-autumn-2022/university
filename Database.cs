using System;
using Npgsql;

namespace university
{
    public class Database : IDisposable
    {
        public NpgsqlConnection Connection { get; }

        public Database(string connectionString)
        {
            Connection = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("HEROKU_POSTGRESQL_IVORY_URL"));
        }

        public void Dispose() => Connection.Dispose();
    }
}