using System;

namespace Bringo.HotDeliveryService.Core
{
    public interface IExpirationPolicy
    {
        TimeSpan DeliveryLifetime { get; set; }
        DateTime GetExpirationTime(DateTime now);
        bool IsExpired(Delivery delivery, DateTime now);
    }
}