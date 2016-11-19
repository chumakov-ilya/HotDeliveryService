using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Contracts
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