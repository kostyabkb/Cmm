using System;
using System.ComponentModel.DataAnnotations;
using Cmm.Host.Model;

namespace Cmm.Contracts
{
    /// <summary>
    /// Принимаемое описание событий.
    /// </summary>
    public class EventRequest
    {
        /// <summary>
        /// Описания события.
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Имя события.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Уровень критичности.
        /// </summary>
        public EventLevel Level { get; set; }
    }
}
