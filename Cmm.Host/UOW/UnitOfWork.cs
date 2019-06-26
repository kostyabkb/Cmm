using System;
using System.Data;
using Cmm.Host.Repositories;

namespace Cmm.Host.UOW
{
    /// <inheritdoc/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection;
        private IDeviceRepository deviceRepository;
        private IDeviceEventRepository deviceEventRepository;
        private IEventRepository eventRepository;

        private IDbTransaction transaction;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connection">Соединение.</param>
        public UnitOfWork(IDbConnection connection)
        {
            this.connection = connection;
            transaction = this.connection.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            Rollback();
        }

        public IDeviceRepository GetDeviceRepository()
        {
            if (deviceRepository == null)
                deviceRepository = new DbDeviceRepository(connection, transaction);

            return deviceRepository;
        }

        public IDeviceEventRepository GetEventRepository()
        {
            if (deviceEventRepository == null)
                deviceEventRepository = new DbDeviceEventRepository(connection, transaction);

            return deviceEventRepository;
        }

        public IEventRepository GetDeviceEventRepository()
        {
            if (eventRepository == null)
                eventRepository = new DbEventRepository(connection, transaction);

            return eventRepository;
        }

        public void Rollback()
        {
            transaction?.Dispose();
        }
    }
}
