using System;
using System.ComponentModel.DataAnnotations;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Девайс, присылаемая статистика.
    /// </summary>
    public class DeviceStatistic
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// ОС.
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Os { get; set; }

        /// <summary>
        /// Версия.
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Version { get; set; }
    }
}
