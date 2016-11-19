using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Services
{
    public class DeliveryService
    {
        public IRepository Repository { get; set; }
        public IExpirationPolicy ExpirationPolicy { get; set; }

        public DeliveryService(IRepository repository, IExpirationPolicy expirationPolicy)
        {
            Repository = repository;
            ExpirationPolicy = expirationPolicy;
        }

        private static readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

        public async Task<Delivery> GetByIdAsync(int deliveryId)
        {
            return await Repository.GetByIdAsync(deliveryId);
        }

        public async Task<Delivery> TakeAsync(int deliveryId, int userId)
        {
            Delivery delivery = await Repository.GetByIdAsync(deliveryId);

            if (delivery == null) return null;

            if (TakeIsDenied(delivery)) return delivery;

            //locking ExpireJob
            await _mutex.WaitAsync();
            try
            {
                //double checking, sad but true
                delivery = await Repository.GetByIdAsync(deliveryId);

                if (TakeIsDenied(delivery)) return delivery;

                delivery.Status = DeliveryStatusEnum.Taken;
                delivery.UserId = userId;

                await Repository.SaveAsync(delivery);

                return delivery;
            }
            finally
            {
                _mutex.Release();
            }
        }

        private bool TakeIsDenied(Delivery delivery)
        {
            return delivery.Status == DeliveryStatusEnum.Taken ||
                   ExpirationPolicy.IsExpired(delivery, DateTime.Now);
        }

        public async Task<List<Delivery>> GetAsync(Filter filter = null)
        {
            return await Repository.GetAsync(filter);
        }

        public async Task MarkAsExpiredAsync()
        {
            await _mutex.WaitAsync();
            try
            {
                DateTime expirationTime = ExpirationPolicy.GetExpirationTime(DateTime.Now);

                await Repository.MarkAsExpiredAsync(expirationTime);
            }
            finally
            {
                _mutex.Release();
            }
        }

        /// <summary>
        /// Save new entities without any kind of locking.
        /// </summary>
        /// <param name="deliveries"></param>
        /// <returns></returns>
        public async Task InsertAsync(ICollection<Delivery> deliveries)
        {
            bool newOnly = deliveries.All(d => d.Id == 0);

            if (!newOnly) throw new NotSupportedException("Only new entities are allowed in Insert operation");

            await Repository.SaveAsync(deliveries);
        }
    }
}