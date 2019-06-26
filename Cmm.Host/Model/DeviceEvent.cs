using System;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Событие.
    /// </summary>
    public class DeviceEvent
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Время.
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Id устройства.
        /// </summary>
        public Guid DeviceId { get; set; }
    }
}
