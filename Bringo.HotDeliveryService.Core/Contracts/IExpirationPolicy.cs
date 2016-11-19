using System;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Contracts
{
    public interface IExpirationPolicy
    {
        TimeSpan DeliveryLifetime { get; set; }
        DateTime GetExpirationTime(DateTime now);
        bool IsExpired(Delivery delivery, DateTime now);
    }
}