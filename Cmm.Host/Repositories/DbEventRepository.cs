using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cmm.Host.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Cmm.Host.Repositories
{
    /// <inheritdoc/>
    public class DbEventRepository : IEventRepository
    {
        private readonly string connectionString;

        private IDbConnection Connection => new NpgsqlConnection(connectionString);

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public DbEventRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionString"];
        }

        public void Add(Event newEvent)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO public.events (id, name, date, device_id) VALUES (@Id, @Name, @Date, @DeviceId)", newEvent);
            }
        }

        public List<Event> Get()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Event>("SELECT id, name, date, device_id FROM public.events;").ToList();
            }
        }
    }
}
