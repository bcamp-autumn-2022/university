using System;
using MySqlConnector;

namespace university
{
    public class Database : IDisposable
    {
        public MySqlConnection Connection { get; }

        public Database(string connectionString)
        {
            //Connection = new MySqlConnection(connectionString);
            Connection = new MySqlConnection(Environment.GetEnvironmentVariable("$DATABASE_URL"));
        }

        public void Dispose() => Connection.Dispose();
    }
}