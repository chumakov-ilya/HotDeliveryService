using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biggy.Core;
using Biggy.Data.Json;

namespace Bringo.HotDeliveryService.Core
{
    public class JsonRepository : IRepository
    {
        public IAppSettings Settings { get; set; }

        public JsonRepository(IAppSettings settings)
        {
            Settings = settings;
        }

        private BiggyList<Delivery> GetList()
        {
            var store = new JsonStore<Delivery>(Settings.GetStoragePath(), "", nameof(Delivery));

            return new BiggyList<Delivery>(store);
        }

        public async Task ClearAllAsync()
        {
            await Task.Run(() => GetList().Clear()).ConfigureAwait(false);
        }

        public async Task SaveAsync(Delivery delivery)
        {
            await SaveAsync(new [] {delivery});
        }

        public async Task<List<Delivery>> GetAsync(Filter filter = null)
        {
            return await Task.Run(() => GetList().Where(d => filter == null || d.Status == filter.Status).ToList()).ConfigureAwait(false);
        }

        public async Task SaveAsync(ICollection<Delivery> deliveries)
        {
            await Task.Run(() =>
            {
                var list = GetList();

                foreach (var delivery in deliveries)
                {
                    delivery.MarkAsModified();

                    if (delivery.Id == 0)
                    {
                        delivery.Id = list.Count > 0 ? list.Max(d => d.Id) + 1 : 1;
                        list.Add(delivery);
                    }
                    else
                    {
                        list.Update(delivery);
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task MarkAsExpiredAsync(DateTime expirationTime)
        {
            await Task.Run(() =>
            {
                var list = GetList();

                var expired = list.Where(d => d.IsExpiredByTime(expirationTime)).ToList();

                expired.ForEach(d => d.Status = DeliveryStatusEnum.Expired);
                expired.ForEach(d => d.MarkAsModified());

                list.Update(expired);
            }).ConfigureAwait(false);
        }

        public async Task<Delivery> GetByIdAsync(int deliveryId)
        {
            return await Task.Run(() => GetList().FirstOrDefault(d => d.Id == deliveryId)).ConfigureAwait(false);
        }
    }
}