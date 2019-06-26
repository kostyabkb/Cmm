using System;

namespace Cmm.Host.UOW
{
    /// <summary>
    /// Фабрика UOW.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Создание.
        /// </summary>
        /// <returns>UOW.</returns>
        IUnitOfWork Create();
    }
}
