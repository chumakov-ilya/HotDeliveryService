using System.Threading.Tasks;
using NUnit.Framework;
using SQLite;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class Sqlite_SmokeTests
    {
        [Test]
        public async Task MyMethod()
        {
            var db = new SQLiteAsyncConnection("X:\\Delivery.db", true);

            await db.UpdateAsync(new Delivery());
        }

        [Test]
        public void CreateDb()
        {
            var db = new SQLiteConnection("X:\\Delivery.db", true);
            db.CreateTable<Delivery>();
            db.Dispose();
        }
    }
}