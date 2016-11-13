using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Configs;

namespace Bringo.HotDeliveryService.Core
{
    public class CreateJob : IJob
    {
        public CreateJob(IRepository repository, RandomDeliveryPolicy policy)
        {
            Repository = repository;
            Policy = policy;
        }

        public RandomDeliveryPolicy Policy { get; set; }

        public IRepository Repository { get; set; }

        public async Task RunAsync()
        {
            int countToCreate = Policy.GetDeliveryCount();

            var deliveries = CreateDeliveries(countToCreate);

            Trace.WriteLine($"Creating {countToCreate} deliveries.");

            await Repository.Save(deliveries);
        }

        private List<Delivery> CreateDeliveries(int count)
        {
            return Enumerable.Repeat(0, count).Select(x => CreateDelivery()).ToList();
        }

        private Delivery CreateDelivery()
        {
            var delivery = new Delivery();
            delivery.Title = Guid.NewGuid().ToString();
            delivery.Status = DeliveryStatusEnum.Available;
            delivery.CreationTime = DateTime.Now;
            return delivery;
        }
    }
}