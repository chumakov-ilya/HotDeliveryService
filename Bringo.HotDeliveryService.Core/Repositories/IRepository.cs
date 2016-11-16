﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public interface IRepository
    {
        Task ClearAll();

        Task Save(List<Delivery> deliveries);

        Task<List<Delivery>> GetAll();

        Task MarkAsExpired(DateTime expirationTime);

        Task<Delivery> GetById(int deliveryId);
    }
}