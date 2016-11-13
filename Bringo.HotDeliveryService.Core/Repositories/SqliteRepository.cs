using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Configs;
using SQLite;

namespace Bringo.HotDeliveryService.Core
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
            string path = Settings.StoragePath + "Delivery.sqlite";

            return new SQLiteAsyncConnection(path);
        }

        public async Task Save(List<Delivery> deliveries)
        {
            var db = CreateConnection();

            await db.UpdateAllAsync(deliveries.Where(d => d.Id > 0));

            await db.InsertAllAsync(deliveries.Where(d => d.Id == 0));
        }

        public async Task<List<Delivery>> ReadAll()
        {
            var db = CreateConnection();

            var list = await db.Table<Delivery>().ToListAsync();

            return list;
        }

        public async Task UpdateExpired(DateTime expirationTime)
        {
            var db = CreateConnection();

            await db.ExecuteAsync(
                "UPDATE Delivery SET Status = 3 WHERE Status = 1 AND CreationTime <= @expTime", 
                expirationTime);
        }

        public async Task ClearAll()
        {
            var db = CreateConnection();

            await db.ExecuteAsync($"DELETE FROM {nameof(Delivery)}");
        }

    }
}