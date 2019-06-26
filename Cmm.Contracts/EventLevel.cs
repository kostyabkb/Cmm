using System;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Уровень критичности события.
    /// </summary>
    public enum EventLevel : byte
    {
        /// <summary>
        /// Неопределен.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Низкий.
        /// </summary>
        Low = 1,

        /// <summary>
        /// Средний.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Высокий.
        /// </summary>
        High = 3
    }
}
