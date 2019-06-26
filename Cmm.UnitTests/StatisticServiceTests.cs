using System;
using System.Collections.Generic;
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
    public class StatisticServiceTests
    {
        private readonly Mock<IDeviceRepository> deviceRepositoryMock;
        private readonly Mock<IDeviceEventRepository> eventRepositoryMock;
        private readonly Mock<IEventRepository> deviceEventRepositoryMock;

        private readonly Guid existId;
        private readonly Guid notExistId;

        private readonly string existName;
        private readonly string notExistName;

        private readonly IStatisticService statisticService;
        private readonly Mock<IUnitOfWorkFactory> uowFactoryMock;
        private readonly Mock<IUnitOfWork> uowMock;

        private readonly Mock<INotificationService> notificationServiceMock;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public StatisticServiceTests()
        {
            notExistId = Guid.NewGuid();
            existId = Guid.NewGuid();

            existName = "exist";
            notExistName = "notexist";

            deviceRepositoryMock = new Mock<IDeviceRepository>();
            eventRepositoryMock = new Mock<IDeviceEventRepository>();
            deviceEventRepositoryMock = new Mock<IEventRepository>();

            deviceRepositoryMock.Setup(x => x.GetById(notExistId)).Returns(Task.FromResult<Device>(null));
            deviceRepositoryMock.Setup(x => x.GetById(existId)).Returns(Task.FromResult(new Device()));

            deviceEventRepositoryMock.Setup(x => x.GetByName(existName)).Returns(Task.FromResult<Event>(new Event()));
            deviceEventRepositoryMock.Setup(x => x.GetByName(notExistName)).Returns(Task.FromResult<Event>(null));


            uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetDeviceRepository()).Returns(() => deviceRepositoryMock.Object);
            uowMock.Setup(x => x.GetEventRepository()).Returns(() => eventRepositoryMock.Object);
            uowMock.Setup(x => x.GetDeviceEventRepository()).Returns(() => deviceEventRepositoryMock.Object);

            uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            uowFactoryMock.Setup(x => x.Create()).Returns(() => uowMock.Object);

            notificationServiceMock = new Mock<INotificationService>();

            statisticService = new StatisticService(uowFactoryMock.Object, notificationServiceMock.Object);
        }

        /// <summary>
        /// Тест на вызов метода обновления устройства если Id существует в базе.
        /// </summary>
        [Fact]
        public async Task SaveDevice_Existed_CallUpdate()
        {
            DeviceRequest device = GetDevice(existId);

            await statisticService.Save(device);

            deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);

            deviceRepositoryMock.Verify(x => x.Add(It.IsAny<Device>()), Times.Never);
        }

        /// <summary>
        /// Тест на вызов метода добавления устройства если Id не существует в базе.
        /// </summary>
        [Fact]
        public async Task SaveDevice_NotExisted_CallAdd()
        {
            DeviceRequest device = GetDevice(notExistId);

            await statisticService.Save(device);

            deviceRepositoryMock.Verify(x => x.Add(It.IsAny<Device>()), Times.Once);
            deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Never);
        }

        /// <summary>
        /// Тест на присваиваемость Id полю Id.device события.
        /// </summary>
        [Fact]
        public async Task SaveEventEquals_Ok()
        {
            var deviceId = new Guid();

            var deviceEvent = new DeviceEventRequest
            {
                Name = "start_app",
                Date = DateTimeOffset.Now
            };

            var deviceEvents = new List<DeviceEventRequest> { deviceEvent };

            var device = new DeviceRequest
            {
                Id = deviceId,
                DeviceEvents = deviceEvents,
                Name = "somename",
                Os = "someOs",
                Version = "later"
            };

            await statisticService.Save(device);

            eventRepositoryMock.Verify(x => x.Add(It.Is<DeviceEvent>(c => c.DeviceId == deviceId)), Times.Once);
        }

        /// <summary>
        /// Тест на вызов метода Адд при несуществующем описании события.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task SaveDeviceEvent_NotExisted_CallAdd()
        {
            DeviceRequest device = GetDevice(existName);
            await statisticService.Save(device);
            deviceEventRepositoryMock.Verify(x => x.Add(It.IsAny<string>()), Times.Never);

            device = GetDevice(notExistName);
            await statisticService.Save(device);
            deviceEventRepositoryMock.Verify(x => x.Add(It.IsAny<string>()), Times.Once);
        }

        private DeviceRequest GetDevice(Guid id)
        {
            return new DeviceRequest
            {
                Id = id,
                Name = "TestName",
                Os = "OsX",
                Version = "1",
                DeviceEvents = new List<DeviceEventRequest>
                {
                    new DeviceEventRequest
                    {
                        Name = "some_action",
                        Date = DateTimeOffset.Now
                    }
                }
            };
        }

        private DeviceRequest GetDevice(string eventName)
        {
            return new DeviceRequest
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                Os = "OsX",
                Version = "1",
                DeviceEvents = new List<DeviceEventRequest>
                {
                    new DeviceEventRequest
                    {
                        Name = eventName,
                        Date = DateTimeOffset.Now
                    }
                }
            };
        }
    }
}
