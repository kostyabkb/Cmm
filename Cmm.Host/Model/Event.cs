using System;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Описание событий.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Описания события.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Имя события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровень критичности.
        /// </summary>
        public EventLevel Level { get; set; }
    }
}
