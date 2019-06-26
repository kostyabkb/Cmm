using System;

namespace Cmm.Contracts
{
    /// <summary>
    /// Устройство, отправляемая статистика.
    /// </summary>
    public class DeviceResponse
    {
        /// <summary>
        /// Id устройства.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ОС.
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Версия.
        /// </summary>
        public string Version { get; set; }
    }
}
