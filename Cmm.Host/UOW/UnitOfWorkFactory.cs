using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Cmm.Host.UOW
{
    /// <inheritdoc/>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string connectionString;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public UnitOfWorkFactory(IConfiguration configuration)
        {
            connectionString = configuration["connectionString"];
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(ProvideConnection());
        }

        private IDbConnection ProvideConnection()
        {
            var connection = new NpgsqlConnection(connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}
