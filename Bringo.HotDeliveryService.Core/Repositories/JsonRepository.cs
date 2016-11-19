using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biggy.Core;
using Biggy.Data.Json;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Repositories
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

        public Task ClearAllAsync()
        {
            GetList().Clear();
            return Task.CompletedTask;
        }

        public Task SaveAsync(Delivery delivery)
        {
            return SaveAsync(new [] {delivery});
        }

        public Task<List<Delivery>> GetAsync(Filter filter = null)
        {
            var deliveries = GetList().Where(d => filter == null || d.Status == filter.Status).ToList();

            return Task.FromResult(deliveries);
        }

        public Task SaveAsync(ICollection<Delivery> deliveries)
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

            return Task.CompletedTask;
        }

        public Task MarkAsExpiredAsync(DateTime expirationTime)
        {
            var list = GetList();

            var expired = list.Where(d => d.ShouldBeExpired(expirationTime)).ToList();

            expired.ForEach(d => d.Status = DeliveryStatusEnum.Expired);
            expired.ForEach(d => d.MarkAsModified());

            list.Update(expired);

            return Task.CompletedTask;
        }

        public Task<Delivery> GetByIdAsync(int deliveryId)
        {
            Delivery delivery = GetList().FirstOrDefault(d => d.Id == deliveryId);

            return Task.FromResult(delivery);
        }
    }
}