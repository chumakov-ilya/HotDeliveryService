using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class Repository_SmokeTests
    {
        public IRepository Repo { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            //Repo = DiRoot.Resolve<SqliteRepository>();
            Repo = DiRoot.Resolve<JsonRepository>();

            await Repo.ClearAll();
        }

        [Test]
        public async Task Get()
        {
            await Add();

            var fromDb = await Repo.Get();

            var delivery = await Repo.GetById(fromDb.Select(d => d.Id).First());

            Assert.IsNotNull(delivery);
        }

        [Test]
        public async Task Add()
        {
            var delivery = new Delivery() { Id = 0 };

            var toDb = new[] { delivery };

            await Repo.Save(toDb.ToList());

            var fromDb = await Repo.Get();

            Assert.IsTrue(fromDb.Any(d => d.Id == delivery.Id));
        }

        [Test]
        public async Task Update()
        {
            var fromDb = await Repo.Get();

            var delivery = fromDb.LastOrDefault();

            if (delivery == null)
            {
                await Add();
                fromDb = await Repo.Get();
                delivery = fromDb.Last();
            }

            delivery.Title = Gen.Text;

            var toDb = new[] { delivery };

            await Repo.Save(toDb.ToList());

            fromDb = await Repo.Get();

            Assert.IsTrue(fromDb.First(d => d.Id == delivery.Id).Title == delivery.Title);
        }

        [Test]
        public async Task GetAll()
        {
            var deliveries = await Repo.Get(new Filter() {Status = DeliveryStatusEnum.Taken});

            Console.WriteLine(deliveries.Serialize());
        }

        [Test]
        public async Task Clear()
        {
            await Repo.ClearAll();

            var deliveries = await Repo.Get();

            Assert.IsEmpty(deliveries);
        }

        [Test]
        public async Task UpdateExpired()
        {
            await Repo.MarkAsExpired(DateTime.Now);
        }
    }
}