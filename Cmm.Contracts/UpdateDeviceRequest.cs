using System;
using System.ComponentModel.DataAnnotations;

namespace Cmm.Contracts
{
    /// <summary>
    /// Модель обновления имени устройства.
    /// </summary>
    public class UpdateDeviceRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Новое имя.
        /// </summary>
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
