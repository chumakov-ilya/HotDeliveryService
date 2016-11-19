using System;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Configs
{
    public class ExpirationPolicy : IExpirationPolicy
    {
        public TimeSpan DeliveryLifetime { get; set; }

        public ExpirationPolicy(IAppSettings settings)
        {
            DeliveryLifetime = TimeSpan.FromSeconds(settings.DeliveryLifetime);
        }

        public DateTime GetExpirationTime(DateTime now)
        {
            return now - DeliveryLifetime;
        }

        public bool IsExpired(Delivery delivery, DateTime now)
        {
            //before this time ALL deliveries already have Expired status (or should be have).
            DateTime expirationTime = GetExpirationTime(now);

            return delivery.Status == DeliveryStatusEnum.Expired || delivery.ShouldBeExpired(expirationTime);
        }
    }
}