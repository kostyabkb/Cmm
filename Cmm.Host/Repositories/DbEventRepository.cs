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
    public class DbEventRepository : IEventRepository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connection">Соединение.</param>
        /// <param name="transaction">Транзакция.</param>
        public DbEventRepository(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task Add(string eventName)
        {
            await connection.ExecuteAsync(
                "INSERT INTO public.events(name) VALUES (@Name)",
                new { Name = eventName },
                transaction);
        }

        public async Task<List<Event>> Get()
        {
            IEnumerable<Event> response = await connection.QueryAsync<Event>(
                "SELECT name, description, level FROM public.events",
                transaction);
            return response.ToList();
        }

        public async Task<Event> GetByName(string name)
        {
            IEnumerable<Event> response = await connection.QueryAsync<Event>(
                "SELECT name, description, level FROM public.events WHERE name = @Name",
                new { Name = name },
                transaction);

            return response.FirstOrDefault();
        }

        public async Task Update(Event @event)
        {
            await connection.ExecuteAsync(
                "UPDATE public.events SET description = @Description, level=@Level WHERE name = @Name",
                @event,
                transaction);
        }
    }
}
