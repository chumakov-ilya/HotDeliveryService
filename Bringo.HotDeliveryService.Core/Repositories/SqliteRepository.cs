using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace Bringo.HotDeliveryService.Core
{
    public class SqliteRepository : IRepository
    {
        private string _filename = "X:\\Delivery.db";

        public async Task Save(List<Delivery> deliveries)
        {
            var db = new SQLiteAsyncConnection(_filename);

            await db.UpdateAllAsync(deliveries.Where(d => d.Id > 0));

            await db.InsertAllAsync(deliveries.Where(d => d.Id == 0));
        }

        public async Task<List<Delivery>> ReadAll()
        {
            var db = new SQLiteAsyncConnection(_filename);

            var list = await db.Table<Delivery>().ToListAsync();

            return list;
        }

        public async Task UpdateExpired(DateTime expirationTime)
        {
            var db = new SQLiteAsyncConnection(_filename);

            await db.ExecuteAsync(
                "UPDATE Delivery SET Status = 3 WHERE Status = 1 AND CreationTime <= @expTime", 
                expirationTime);
        }

        public async Task ClearAll()
        {
            var db = new SQLiteAsyncConnection(_filename);

            await db.ExecuteAsync($"DELETE FROM {nameof(Delivery)}");
        }

    }
}