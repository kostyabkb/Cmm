using System;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.Services;
using Cmm.Host.UOW;
using Moq;
using Xunit;

namespace Cmm.UnitTests
{
    /// <summary>
    /// Unit тесты на сервис устройств DevicesService.
    /// </summary>
    public class DevicesServiceTests
    {
        private readonly Mock<IDeviceRepository> devicesRepositoryMock;

        private readonly Guid existId;
        private readonly Guid notExistId;

        private readonly IDevicesService service;
        private readonly Mock<IUnitOfWorkFactory> uowFactoryMock;
        private readonly Mock<IUnitOfWork> uowMock;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DevicesServiceTests()
        {
            notExistId = Guid.NewGuid();
            existId = Guid.NewGuid();

            devicesRepositoryMock = new Mock<IDeviceRepository>();
            devicesRepositoryMock.Setup(x => x.GetById(notExistId)).Returns(Task.FromResult<Device>(null));
            devicesRepositoryMock.Setup(x => x.GetById(existId)).Returns(Task.FromResult(new Device()));

            uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetDeviceRepository()).Returns(() => devicesRepositoryMock.Object);

            uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            uowFactoryMock.Setup(x => x.Create()).Returns(() => uowMock.Object);

            service = new DevicesService(uowFactoryMock.Object);
        }

        /// <summary>
        /// Тест на вызов метода обновления устройства если Id существует в базе.
        /// </summary>
        [Fact]
        public async Task RenameDevice_Existed_CallUpdate()
        {
            var notExistDevice = GetDevice(notExistId);
            await Assert.ThrowsAsync<Exception>(() => service.Rename(notExistDevice));
            devicesRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Never);

            var device = GetDevice(existId);
            await service.Rename(device);
            devicesRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        }

        private UpdateDeviceRequest GetDevice(Guid id)
        {
            return new UpdateDeviceRequest
            {
                Id = id,
                Name = "TestName"
            };
        }
    }
}
