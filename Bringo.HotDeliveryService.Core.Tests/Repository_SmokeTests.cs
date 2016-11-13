using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class Repository_SmokeTests
    {
        [SetUp]
        public void SetUp()
        {
            //Repo = new FileRepository();
            Repo = new JsonRepository();
            //Repo = new SqliteRepository();
        }

        public IRepository Repo { get; set; }

        [Test]
        public async Task Add()
        {
            var delivery = new Delivery() { Id = 0 };

            var toDb = new[] { delivery };

            await Repo.Save(toDb.ToList());

            var fromDb = await Repo.ReadAll();

            Assert.IsTrue(fromDb.Any(d => d.Id == delivery.Id));
        }

        [Test]
        public async Task Update()
        {
            var fromDb = await Repo.ReadAll();

            var delivery = fromDb.LastOrDefault();

            if (delivery == null)
            {
                await Add();
                fromDb = await Repo.ReadAll();
                delivery = fromDb.Last();
            }

            delivery.Title = Gen.Text;

            var toDb = new[] { delivery };

            await Repo.Save(toDb.ToList());

            fromDb = await Repo.ReadAll();

            Assert.IsTrue(fromDb.First(d => d.Id == delivery.Id).Title == delivery.Title);
        }

        [Test]
        public async Task ReadAll()
        {
            var deliveries = await Repo.ReadAll();

            Console.WriteLine(deliveries.Serialize());
        }

        [Test]
        public async Task Clear()
        {
            await Repo.ClearAll();

            var deliveries = await Repo.ReadAll();

            Assert.IsEmpty(deliveries);
        }

        [Test]
        public async Task UpdateExpired()
        {
            await Repo.UpdateExpired(DateTime.Now);
        }
    }
}