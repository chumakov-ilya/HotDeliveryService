using System;
using System.Linq;
using System.Threading.Tasks;
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
            Repository = DiRoot.Resolve(RepositoryType) as IRepository;
            Factory = new DeliveryFactory();
        }

        [SetUp]
        public async Task SetUp()
        {
            await Repository.ClearAll();
        }

        [Test]
        public async Task Get()
        {
            await Add();

            var fromDb = await Repository.Get();

            var delivery = await Repository.GetById(fromDb.Select(d => d.Id).First());

            Assert.IsNotNull(delivery);
        }

        [Test]
        public async Task Add()
        {
            var delivery = Factory.CreateDelivery();

            var toDb = new[] { delivery };

            await Repository.Save(toDb.ToList());

            var fromDb = await Repository.Get();

            Assert.IsTrue(fromDb.Any(d => d.Id == delivery.Id));
        }

        [Test]
        public async Task Update()
        {
            var fromDb = await Repository.Get();

            var delivery = fromDb.LastOrDefault();

            if (delivery == null)
            {
                await Add();
                fromDb = await Repository.Get();
                delivery = fromDb.Last();
            }

            delivery.Title += Gen.Text;

            var toDb = new[] { delivery };

            await Repository.Save(toDb.ToList());

            fromDb = await Repository.Get();

            Assert.IsTrue(fromDb.First(d => d.Id == delivery.Id).Title == delivery.Title);
        }

        [Test]
        public async Task GetAll()
        {
            var deliveries = await Repository.Get(new Filter() {Status = DeliveryStatusEnum.Taken});

            Console.WriteLine(deliveries.Serialize());
        }

        [Test]
        public async Task Clear()
        {
            await Repository.ClearAll();

            var deliveries = await Repository.Get();

            Assert.IsEmpty(deliveries);
        }

        [Test]
        public async Task UpdateExpired()
        {
            await Repository.MarkAsExpired(DateTime.Now);
        }
    }
}