using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Configs;

namespace Bringo.HotDeliveryService.Core
{
    public class FileRepository : IRepository
    {
        private string _path = "X:\\db.json";

        public async Task ClearAll()
        {
            await Overwrite(new List<Delivery>());
        }

        public async Task Save(List<Delivery> deliveries)
        {
            var oldRecords = await GetAll();

            var newIds = deliveries.Select(d => d.Id);

            var merged = deliveries.Select(x => x).ToList();

            merged.AddRange(oldRecords.Where(d => !newIds.Contains(d.Id)));

            int lastId = merged.Select(d => d.Id).Max();

            foreach (Delivery newDelivery in merged.Where(d => d.Id == 0))
            {
                newDelivery.Id = ++lastId;
            }

            await Overwrite(merged);
        }

        private async Task Overwrite(List<Delivery> deliveries)
        {
            string json = deliveries.Serialize();

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                await writer.WriteAsync(json);
            }
        }

        public async Task<List<Delivery>> GetAll()
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                string json = await reader.ReadToEndAsync();

                return json.Deserialize<List<Delivery>>() ?? new List<Delivery>();
            }
        }

        public Task MarkAsExpired(DateTime expirationTime)
        {
            throw new NotImplementedException();
        }

        public Task<Delivery> GetById(int deliveryId)
        {
            throw new NotImplementedException();
        }
    }
}