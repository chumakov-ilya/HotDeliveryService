using System;
using System.Collections.Generic;
using System.Linq;
using Bringo.HotDeliveryService.Core.Model;

namespace Bringo.HotDeliveryService.Core.Services
{
    public class DeliveryFactory
    {
        public List<Delivery> CreateDeliveries(int count)
        {
            return Enumerable.Repeat(0, count).Select(x => CreateDelivery()).ToList();
        }

        public Delivery CreateDelivery()
        {
            var delivery = new Delivery();
            delivery.Title = Guid.NewGuid().ToString();
            delivery.Status = DeliveryStatusEnum.Available;
            delivery.CreationTime = DateTime.Now;
            return delivery;
        }
    }
}