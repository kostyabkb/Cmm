using System;
using System.ComponentModel.DataAnnotations;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Девайс, хранится в базе.
    /// </summary>
    public class Device
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
        /// ОС.
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Версия.
        /// </summary>
        public string Version { get; set; }
    }
}
