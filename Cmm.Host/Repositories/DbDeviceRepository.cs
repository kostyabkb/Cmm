using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cmm.Host.Model;
using Dapper;

namespace Cmm.Host.Repositories
{
    /// <inheritdoc/>
    public class DbDeviceRepository : IDeviceRepository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connection">Соединение.</param>
        /// <param name="transaction">Транзакция.</param>
        public DbDeviceRepository(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task Add(Device device)
        {
            await connection.ExecuteAsync(
                "INSERT INTO public.devices (id, name, os, version) VALUES(@Id, @Name, @Os, @Version)",
                device,
                transaction);
        }

        public async Task<List<Device>> Get()
        {
            IEnumerable<Device> response = await connection.QueryAsync<Device>(
                "SELECT os, name, id, version FROM public.devices",
                transaction);

            return response.ToList();
        }

        public async Task<Device> GetById(Guid deviceId)
        {
            IEnumerable<Device> response = await connection.QueryAsync<Device>(
                "SELECT os, name, id, version FROM public.devices WHERE id = @Id",
                new { Id = deviceId },
                transaction);

            return response.FirstOrDefault();
        }

        public async Task Update(Device device)
        {
            await connection.ExecuteAsync(
                "UPDATE public.devices SET name = @Name, os = @Os, version = @Version WHERE id = @Id",
                device,
                transaction);
        }
    }
}
