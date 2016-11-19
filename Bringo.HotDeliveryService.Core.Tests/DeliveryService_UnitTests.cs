using System;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;
using Bringo.HotDeliveryService.Core.Services;
using Moq;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class DeliveryService_UnitTests
    {
        [Test]
        public async Task TakeAsync_AvailableDelivery_ChangedToTaken()
        {
            var service = CreateDeliveryService(deliveryStatus: DeliveryStatusEnum.Available);

            Delivery delivery = await service.TakeAsync(1, 1);

            Assert.AreEqual(DeliveryStatusEnum.Taken, delivery.Status);
        }

        [Test]
        public async Task TakeAsync_ExpiredDelivery_NoChange()
        {
            var service = CreateDeliveryService(deliveryStatus: DeliveryStatusEnum.Expired);

            Delivery delivery = await service.TakeAsync(1, 1);

            Assert.AreEqual(DeliveryStatusEnum.Expired, delivery.Status);
        }

        [Test]
        public async Task TakeAsync_TakenDelivery_NoChange()
        {
            var service = CreateDeliveryService(deliveryStatus: DeliveryStatusEnum.Taken);

            Delivery delivery = await service.TakeAsync(1, 1);

            Assert.AreEqual(DeliveryStatusEnum.Taken, delivery.Status);
        }

        private static DeliveryService CreateDeliveryService(DeliveryStatusEnum deliveryStatus)
        {
            var factory = new DeliveryFactory();
            Delivery delivery = factory.CreateDelivery();
            delivery.Status = deliveryStatus;

            bool deliveriesAreExpired = deliveryStatus != DeliveryStatusEnum.Available;

            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(delivery);
            var policy = new Mock<IExpirationPolicy>();

            policy.Setup(m => m.IsExpired(It.IsAny<Delivery>(), It.IsAny<DateTime>())).Returns(deliveriesAreExpired);

            return new DeliveryService(repository.Object, policy.Object);
        }
    }
}