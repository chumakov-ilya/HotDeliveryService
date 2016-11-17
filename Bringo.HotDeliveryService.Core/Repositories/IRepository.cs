using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public interface IRepository
    {
        Task ClearAll();

        Task Save(Delivery delivery);

        Task Save(ICollection<Delivery> deliveries);

        Task<List<Delivery>> Get(Filter filter = null);

        Task MarkAsExpired(DateTime expirationTime);

        Task<Delivery> GetById(int deliveryId);
    }
}