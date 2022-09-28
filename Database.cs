using System;
using Npgsql;

namespace university
{
    public class Database : IDisposable
    {
        public NpgsqlConnection Connection { get; }

        public Database(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}