using System;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;
using Bringo.HotDeliveryService.Core.Repositories;
using Bringo.HotDeliveryService.Core.Services;
using Bringo.HotDeliveryService.Core.Tools;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    [TestFixture(typeof(JsonRepository))]
    [TestFixture(typeof(SqliteRepository))]
    public class Repository_IntegrationTests
    {
        public Type RepositoryType { get; set; }
        public IRepository Repository { get; set; }
        public DeliveryFactory Factory { get; set; }

        public Repository_IntegrationTests(Type repositoryType)
        {
            RepositoryType = repositoryType;
            Repository = Root.Resolve(RepositoryType) as IRepository;
            Factory = Root.Resolve<DeliveryFactory>();
        }

        [SetUp]
        public async Task SetUp()
        {
            await Repository.ClearAllAsync();
        }

        [Test]
        public async Task Get()
        {
            await Add();

            var fromDb = await Repository.GetAsync();

            var delivery = await Repository.GetByIdAsync(fromDb.Select(d => d.Id).First());

            Assert.IsNotNull(delivery);
        }

        [Test]
        public async Task Add()
        {
            var delivery = Factory.CreateDelivery();

            var toDb = new[] { delivery };

            await Repository.SaveAsync(toDb.ToList());

            var fromDb = await Repository.GetAsync();

            Assert.IsTrue(fromDb.Any(d => d.Id == delivery.Id));
        }

        [Test]
        public async Task Update()
        {
            var fromDb = await Repository.GetAsync();

            var delivery = fromDb.LastOrDefault();

            if (delivery == null)
            {
                await Add();
                fromDb = await Repository.GetAsync();
                delivery = fromDb.Last();
            }

            delivery.Title += Gen.Text;

            var toDb = new[] { delivery };

            await Repository.SaveAsync(toDb.ToList());

            fromDb = await Repository.GetAsync();

            Assert.IsTrue(fromDb.First(d => d.Id == delivery.Id).Title == delivery.Title);
        }

        [Test]
        public async Task GetAll()
        {
            var deliveries = await Repository.GetAsync(new Filter { Status = DeliveryStatusEnum.Taken });

            Console.WriteLine(deliveries.Serialize());
        }

        [Test]
        public async Task Clear()
        {
            await Repository.ClearAllAsync();

            var deliveries = await Repository.GetAsync();

            Assert.IsEmpty(deliveries);
        }

        [Test]
        public async Task MarkAsExpired()
        {
            await Repository.MarkAsExpiredAsync(DateTime.Now);
        }
    }



}