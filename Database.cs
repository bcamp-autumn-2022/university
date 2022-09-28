using System;
using Npgsql;

namespace university
{
    public class Database : IDisposable
    {
        public NpgsqlConnection Connection { get; }

        public Database(string connectionString)
        {
            Connection = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("POSTGRE_URL"));
        }

        public void Dispose() => Connection.Dispose();
    }
}