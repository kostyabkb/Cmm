﻿using System;

namespace Cmm.Host.Model
{
    /// <summary>
    /// Устройство, хранится в базе.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Идентификатор.
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
