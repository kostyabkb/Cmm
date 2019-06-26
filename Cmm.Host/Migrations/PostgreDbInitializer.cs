using System;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Npgsql;

namespace Cmm.Host.Migrations
{
    /// <summary>
    /// Инициализатор БД.
    /// </summary>
    public static class PostgreDbInitializer
    {
        /// <summary>
        /// Инициализация базы данных.
        /// </summary>
        public static void CheckAndCreate(string connectionString)
        {
            var connectionStringBuilder = new DbConnectionStringBuilder
            {
                ConnectionString = connectionString
            };

            string dbNameKey = GetDbNameKey(connectionStringBuilder);

            var dbName = connectionStringBuilder[dbNameKey] as string;

            connectionStringBuilder[dbNameKey] = "";

            connectionString = connectionStringBuilder.ConnectionString;

            if (!CheckDbExists(connectionString, dbName))
            {
                CreateDb(connectionString, dbName);
            }
        }

        private static bool CheckDbExists(string connectionString, string dbName)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand($"SELECT 1 FROM pg_catalog.pg_database WHERE DATNAME = '{dbName}'", connection))
                {
                    connection.Open();
                    object i = command.ExecuteScalar();
                    return i != null;
                }
            }
        }

        private static void CreateDb(string connectionString, string dbName)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand($@"CREATE DATABASE {dbName} WITH OWNER postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string GetDbNameKey(DbConnectionStringBuilder connectionStringBuilder)
        {
            string dbNameKey = connectionStringBuilder
                .Keys
                .OfType<string>()
                .First(x => x.ToUpper(CultureInfo.CurrentCulture) == "DATABASE");
            return dbNameKey;
        }
    }
}
