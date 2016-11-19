using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;
using SQLite;

namespace Bringo.HotDeliveryService.Core.Repositories
{
    public class SqliteRepository : IRepository
    {
        public IAppSettings Settings { get; set; }

        public SqliteRepository(IAppSettings settings)
        {
            Settings = settings;
        }

        private SQLiteAsyncConnection CreateConnection()
        {
            string path = Path.Combine(Settings.GetStoragePath(), "Delivery.sqlite");

            return new SQLiteAsyncConnection(path);
        }

        public async Task SaveAsync(Delivery delivery)
        {
            await SaveAsync(new[] { delivery });
        }

        public async Task SaveAsync(ICollection<Delivery> deliveries)
        {
            var db = CreateConnection();

            foreach (var d in deliveries) d.MarkAsModified();

            await db.UpdateAllAsync(deliveries.Where(d => d.Id > 0));

            await db.InsertAllAsync(deliveries.Where(d => d.Id == 0));
        }

        public async Task<List<Delivery>> GetAsync(Filter filter = null)
        {
            var db = CreateConnection();

            var query = db.Table<Delivery>();

            if (filter != null) query = query.Where(d => d.Status == filter.Status);

            return await query.ToListAsync();
        }

        public async Task MarkAsExpiredAsync(DateTime expirationTime)
        {
            var db = CreateConnection();

            await db.ExecuteAsync(
                "UPDATE Delivery SET Status = 3, ModificationTime = @now WHERE Status = 1 AND CreationTime <= @expTime",
                DateTime.Now,
                expirationTime);
        }

        public async Task<Delivery> GetByIdAsync(int deliveryId)
        {
            var db = CreateConnection();

            return await db.Table<Delivery>().Where(d => d.Id == deliveryId).FirstOrDefaultAsync();
        }

        public async Task ClearAllAsync()
        {
            var db = CreateConnection();

            await db.ExecuteAsync($"DELETE FROM {nameof(Delivery)}");
        }

    }
}