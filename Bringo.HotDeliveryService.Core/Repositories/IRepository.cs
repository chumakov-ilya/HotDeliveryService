using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public interface IRepository
    {
        Task ClearAllAsync();

        Task SaveAsync(Delivery delivery);

        Task SaveAsync(ICollection<Delivery> deliveries);

        Task<List<Delivery>> GetAsync(Filter filter = null);

        Task MarkAsExpiredAsync(DateTime expirationTime);

        Task<Delivery> GetByIdAsync(int deliveryId);
    }
}