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
    public class DbDeviceEventRepository : IDeviceEventRepository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connection">Соединение.</param>
        /// <param name="transaction">Транзакция.</param>
        public DbDeviceEventRepository(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task Add(DeviceEvent newDeviceEvent)
        {
            await connection.ExecuteAsync(
                "INSERT INTO public.device_event (id, name, date, device_id) VALUES (@Id, @Name, @Date, @DeviceId)",
                newDeviceEvent,
                transaction);
        }

        public async Task<List<EventDescription>> GetForDevice(Guid deviceId)
        {
            var response = await connection.QueryAsync<EventDescription>(
                "SELECT E.id, E.name, E.date, E.device_id, DE.description FROM public.device_event E INNER JOIN public.events DE ON E.name = DE.name WHERE device_id = @Id",
                new { Id = deviceId },
                transaction);

            return response.ToList();
        }

        public async Task DeleteForDevice(Guid deviceId)
        {
            await connection.ExecuteAsync(
                "DELETE FROM public.device_event WHERE device_id = @Id",
                new { Id = deviceId },
                transaction);
        }
    }
}
