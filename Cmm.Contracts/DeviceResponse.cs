namespace Cmm.Contracts
{
    /// <summary>
    /// Девайс, отправляемый.
    /// </summary>
    public class DeviceResponse
    {
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
