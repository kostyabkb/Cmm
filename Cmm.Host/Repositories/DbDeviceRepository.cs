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
    public class DbDeviceRepository : IDeviceRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public DbDeviceRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionString"];
        }

        private IDbConnection Connection => new NpgsqlConnection(connectionString);

        public void Add(Device device)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO public.devices (id, name, os, version) VALUES(@Id, @Name, @Os, @Version)", device);
            }
        }

        public List<Device> Get()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Device>("SELECT os, name, id, version FROM public.devices").ToList();
            }
        }

        public Device GetById(Guid deviceId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Device>("SELECT os, name, id, version FROM public.devices WHERE id = @Id", new { Id = deviceId }).FirstOrDefault();
            }
        }

        public void Update(Device device)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE public.devices SET id = @Id, name = @Name, os = @Os, version = @Version WHERE id = @Id", device);
            }
        }
    }
}
