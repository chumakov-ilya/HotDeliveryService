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
        public BiggyList<Delivery> GetList()
        {
            var store = new JsonStore<Delivery>("X:\\", "Bringo", "Delivery.json");
            var list = new BiggyList<Delivery>(store);
            return list;
        }

        public async Task ClearAll()
        {
            await Task.Run(() => GetList().Clear());
        }

        public async Task<List<Delivery>> ReadAll()
        {
            return await Task.Run(() => GetList().ToList());
        }

        public async Task Save(List<Delivery> deliveries)
        {
            await Task.Run(() =>
            {
                var list = GetList();

                foreach (var delivery in deliveries)
                {
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
            });
        }

        public async Task UpdateExpired(DateTime expirationTime)
        {
            await Task.Run(() =>
            {
                var list = GetList();

                var expired = list.Where(d =>
                        d.Status == DeliveryStatusEnum.Available && d.CreationTime < expirationTime).ToList();

                expired.ForEach(d => d.Status = DeliveryStatusEnum.Expired);

                list.Update(expired);
            });
        }
    }
}