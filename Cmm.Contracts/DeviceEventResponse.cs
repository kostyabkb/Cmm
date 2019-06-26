using System;

namespace Cmm.Contracts
{
    /// <summary>
    /// Отправляемое событие.
    /// </summary>
    public class DeviceEventResponse
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Время.
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        public string Description { get; set; }
    }
}
