using System;
using System.ComponentModel.DataAnnotations;

namespace Cmm.Contracts
{
    /// <summary>
    /// Принимаемое событие.
    /// </summary>
    public class DeviceEventRequest
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Время.
        /// </summary>
        [Required]
        public DateTimeOffset Date { get; set; }
    }
}
